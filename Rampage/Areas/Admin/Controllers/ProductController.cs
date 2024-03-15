using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Areas.Admin.Utilities.Services;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    private readonly MailKitHelper _mailKitHelper;

    private string messageBody = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Yeni Ürün Duyurusu</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f7f7f7;\r\n            padding: 20px;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background-color: #fff;\r\n            padding: 30px;\r\n            border-radius: 5px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            color: #333;\r\n        }\r\n        p {\r\n            color: #555;\r\n            line-height: 1.6;\r\n        }\r\n        .product-image {\r\n            width: 100%;\r\n            max-width: 400px;\r\n            margin: 0 auto;\r\n            display: block;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            background-color: red;\r\n            color: #fff;\r\n            text-decoration: none;\r\n            padding: 10px 20px;\r\n            border-radius: 5px;\r\n            margin-top: 20px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Yeni Ürün Duyurusu</h1>\r\n        <p>Merhaba,</p>\r\n        <p>Size harika bir haberimiz var! Yeni bir ürünümüz piyasaya sürüldü. Bu ürünü hemen keşfedin ve avantajlardan yararlanın.</p>\r\n        <img class=\"product-image\" src=\"product_image_url\" alt=\"Yeni Ürün\">\r\n        <p>Ürün hakkında daha fazla bilgi almak için aşağıdaki butona tıklayabilirsiniz.</p>\r\n        <a href=\"product_page_url\" class=\"button\">Ürünü Görüntüle</a>\r\n        <p>İyi alışverişler dileriz!</p>\r\n    </div>\r\n</body>\r\n</html>\r\n";
    public ProductController(AppDbContext context, CloudinaryService cloudinaryService, MailKitHelper mailKitHelper)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
        _mailKitHelper = mailKitHelper;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(x => x.Category).Include(x => x.Color).ToListAsync();
        return View(products);
    }


    public async Task<IActionResult> Create()
    {
        var categories = await _context.Categories.Where(x => x.ParentCategoryId != null).ToListAsync();
        var colors = await _context.Colors.ToListAsync();

        ViewBag.Categories = categories;
        ViewBag.Colors = colors;

        return View();

    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductPostVM vm)
    {
        var categories = await _context.Categories.Where(x => x.ParentCategoryId != null).ToListAsync();
        var colors = await _context.Colors.ToListAsync();

        ViewBag.Categories = categories;
        ViewBag.Colors = colors;

        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Burda bir yanlışlık var");
            return View(vm);
        }


        var isExistColor = await _context.Colors.AnyAsync(x => x.Id == vm.ColorId);
        if (!isExistColor)
        {
            ModelState.AddModelError("ColorId", "Burda bir yanlışlık var");
            return View(vm);
        }

        #region ImageValidates

        if (!vm.BaseImage.ValidateImage(2))
        {
            ModelState.AddModelError("BaseImage", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
            return View(vm);
        }



        if (!vm.BGImage.ValidateImage(10))
        {
            ModelState.AddModelError("BGImage", "Resim doğru formatda ve boyutu 10 mb dan az olmalıdır");
            return View(vm);
        }


        foreach (IFormFile image in vm.Images)
        {
            if (!image.ValidateImage(2))
            {
                ModelState.AddModelError("Images", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
                return View(vm);
            }
        }


        #endregion


        Product product = new()
        {
            Name = vm.Name,
            Barcode = vm.Barcode,
            CategoryId = vm.CategoryId,
            Price = vm.Price,
            ColorId = vm.ColorId,
            Count = vm.Count,
        };


        var baseImg = await _cloudinaryService.FileCreateAsync(vm.BaseImage);
        var bgImg = await _cloudinaryService.FileCreateAsync(vm.BGImage);

        product.BaseImagePath = baseImg;
        product.BGImagePath = bgImg;


        List<ProductImage> images = new();

        foreach (IFormFile image in vm.Images)
        {
            var path = await _cloudinaryService.FileCreateAsync(image);

            ProductImage productImage = new()
            {
                ImagePath = path,
                Product = product
            };
            images.Add(productImage);
        }

        product.ProductImages = images;


        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();


        var subscribes = await _context.Subscribes.ToListAsync();

        messageBody = messageBody.Replace("product_image_url", product.BaseImagePath);
        messageBody = messageBody.Replace("product_page_url", "https://www.rampage.com.tr/");
        foreach (var user in subscribes)
        {
            await _mailKitHelper.SendEmailAsync(new()
            {
                Body = messageBody,
                Subject = "Yeni Ürün Duyurusu",
                ToEmail = user.Email

            });

        }





        return RedirectToAction("Index");



    }



    public async Task<IActionResult> Update(int id)
    {
        var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        var categories = await _context.Categories.Where(x => x.ParentCategoryId != null).ToListAsync();
        var colors = await _context.Colors.ToListAsync();

        ViewBag.Categories = categories;
        ViewBag.Colors = colors;

        ProductPutVM vm = new ProductPutVM()
        {
            Id = product.Id,
            Barcode = product.Barcode,
            BaseImagePath = product.BaseImagePath,
            BGImagePath = product.BGImagePath,
            CategoryId = product.CategoryId,
            ColorId = product.ColorId,
            Count = product.Count,
            Name = product.Name,
            Price = product.Price,
            ImagePaths = product.ProductImages.Select(x => x.ImagePath).ToList(),
            ImageIds = product.ProductImages.Select(x => x.Id).ToList()
        };
        //if (product.ProductImages.Count > 0)
        //{

        //    List<string?>? images = new();
        //    List<int>? ids = new();
        //    foreach (var item in product.ProductImages)
        //    {
        //        images.Add(item.ImagePath);
        //        ids.Add(item.Id);
        //    }
        //    vm.ImagePaths = images;
        //    vm.ImageIds = ids;
        //}

        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Update(ProductPutVM vm)
    {
        var existProduct = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (existProduct is null)
            return NotFound();

        var categories = await _context.Categories.Where(x => x.ParentCategoryId != null).ToListAsync();
        var colors = await _context.Colors.ToListAsync();

        ViewBag.Categories = categories;
        ViewBag.Colors = colors;


        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "Burda bir yanlışlık var");
            return View(vm);
        }


        var isExistColor = await _context.Colors.AnyAsync(x => x.Id == vm.ColorId);
        if (!isExistColor)
        {
            ModelState.AddModelError("ColorId", "Burda bir yanlışlık var");
            return View(vm);
        }

        #region ImageValidates



        if (vm.BaseImage is not null && !vm.BaseImage.ValidateImage(2))
        {
            ModelState.AddModelError("BaseImage", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
            return View(vm);
        }



        if (vm.BGImage is not null && !vm.BGImage.ValidateImage(10))
        {
            ModelState.AddModelError("BGImage", "Resim doğru formatda ve boyutu 10 mb dan az olmalıdır");
            return View(vm);
        }


        foreach (IFormFile image in vm.Images)
        {
            if (!image.ValidateImage(2))
            {
                ModelState.AddModelError("Images", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
                return View(vm);
            }
        }


        #endregion



        var ExistedImages = existProduct.ProductImages.Select(x => x.Id).ToList();
        if (vm.ImageIds is not null)
        {
            ExistedImages = ExistedImages.Except(vm.ImageIds).ToList();

        }
        if (ExistedImages.Count > 0)
        {
            foreach (var imageId in ExistedImages)
            {
                var deletedImage = existProduct.ProductImages.FirstOrDefault(x => x.Id == imageId);
                if (deletedImage is not null)
                {

                    existProduct.ProductImages.Remove(deletedImage);
                }

            }
        }

        //Created new Images
        if (vm.Images is not null)
        {
            foreach (var item in vm.Images)
            {
                ProductImage productImage = new() { ImagePath = await _cloudinaryService.FileCreateAsync(item), ProductId = vm.Id };
                existProduct.ProductImages.Add(productImage);

            }
        }
        if (vm.BaseImage is not null)
            existProduct.BaseImagePath = await _cloudinaryService.FileCreateAsync(vm.BaseImage);
        if (vm.BGImage is not null)
            existProduct.BGImagePath = await _cloudinaryService.FileCreateAsync(vm.BGImage);


        existProduct.Name = vm.Name;
        existProduct.Barcode = vm.Barcode;
        existProduct.ColorId = vm.ColorId;
        existProduct.CategoryId = vm.CategoryId;
        existProduct.Price = vm.Price;
        existProduct.Count = vm.Count;




        _context.Update(existProduct);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products.Include(x => x.ProductImages).Include(x => x.Category).Include(x => x.Color).Include(x => x.ProductInfos).FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }



    public async Task<IActionResult> DeleteInfo(int id)
    {
        var info = await _context.ProductInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (info is null)
            return NotFound();

        _context.ProductInfos.Remove(info);
        await _context.SaveChangesAsync();

        return RedirectToAction("Detail", new { Id = info.ProductId });
    }



    public async Task<IActionResult> CreateInfo(int id)
    {
        var isExistProduct = await _context.Products.AnyAsync(x => x.Id == id);
        if (!isExistProduct)
            return NotFound();

        ProductInfoPostVM vm = new();
        vm.ProductId = id;

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInfo(ProductInfoPostVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var isExistProduct = await _context.Products.AnyAsync(x => x.Id == vm.ProductId);
        if (!isExistProduct)
            return NotFound();

        var isExist = await _context.ProductInfos.AnyAsync(x => x.ProductId == vm.ProductId && x.Key.ToLower() == vm.Key.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Key", "Bu anahtarda zaten bir değer var");
            return View(vm);
        }


        ProductInfo info = new() { Key = vm.Key, Value = vm.Value, ProductId = vm.ProductId };

        await _context.ProductInfos.AddAsync(info);
        await _context.SaveChangesAsync();

        return RedirectToAction("Detail", new { id = vm.ProductId });
    }

}
