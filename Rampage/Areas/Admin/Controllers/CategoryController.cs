using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Areas.Admin.Utilities.Services;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    public CategoryController(AppDbContext context, CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IActionResult> Index()
    {
        var categoires = await _context.Categories.Include(x=>x.Products).Include(x=>x.ChildCategories).ToListAsync();
        return View(categoires);
    }


    public async Task<IActionResult> Create()
    {
        var categories = await _context.Categories.Where(x=>x.ParentCategoryId==null).ToListAsync();
        ViewBag.Categories = categories;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CategoryPostVM vm)
    {

        var categories = await _context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync();
        ViewBag.Categories = categories;

        if (!ModelState.IsValid)
            return View(vm);


        var isExist = await _context.Categories.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "Bu adda zaten bir kategori mevcut");
            return View(vm);
        }

        if (vm.ParentCategoryId != 0 && vm.ParentCategoryId != null)
        {
            var isExistCategoryId = await _context.Categories.AnyAsync(x => x.Id == vm.ParentCategoryId && x.ParentCategoryId==null);
            if (!isExistCategoryId)
            {
                ModelState.AddModelError("ParentCategoryId", "Böyle bir kategori bulunmamaktadır.");
                return View(vm);
            }

        }


        var imageValidate = vm.Image.ValidateType();

        if (!imageValidate)
        {
            ModelState.AddModelError("Image", "Lütfen bir resim örneği seçiniz.");
            return View(vm);
        }

        imageValidate = vm.Image.ValidateSize(2);


        if (!imageValidate)
        {
            ModelState.AddModelError("Image", "Resmin maksimum kapasitesi 2 mb olmalı.");
            return View(vm);
        }


        var bgImageValidate = vm.BGImage.ValidateType();

        if (!bgImageValidate)
        {
            ModelState.AddModelError("BGImage", "Lütfen bir resim örneği seçiniz.");
            return View(vm);
        }

        bgImageValidate = vm.BGImage.ValidateSize(2);


        if (!bgImageValidate)
        {
            ModelState.AddModelError("BGImage", "Resmin maksimum kapasitesi 2 mb olmalı.");
            return View(vm);
        }



        string imagePath = await _cloudinaryService.FileCreateAsync(vm.Image);
        string bgImagePath = await _cloudinaryService.FileCreateAsync(vm.BGImage);


        Category category = new()
        {
            Name = vm.Name,
            ImagePath = imagePath,
            BGImagePath = bgImagePath,

        };

        if (vm.ParentCategoryId != 0 && vm.ParentCategoryId is not null)
            category.ParentCategoryId = vm.ParentCategoryId;


        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is null)
            return NotFound();

        var categories = await _context.Categories.Where(x => x.Id != id && x.ParentCategoryId==null).ToListAsync();
        ViewBag.Categories = categories;


        CategoryPutVM vm = new()
        {
            Id = category.Id,
            Name = category.Name,
            ImagePath = category.ImagePath,
            BGImagePath = category.BGImagePath,
            ParentCategoryId = category.ParentCategoryId

        };


        return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryPutVM vm)
    {

        var existedCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (existedCategory is null)
            return NotFound();


        var categories = await _context.Categories.Where(x => x.Id != vm.Id && x.ParentCategoryId == null).ToListAsync();
        ViewBag.Categories = categories;



        if (!ModelState.IsValid)
            return View(vm);


        var isExist = await _context.Categories.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower() && x.Id != vm.Id);
        if (isExist)
        {
            ModelState.AddModelError("Name", "Bu adda zaten bir kategori mevcut");
            return View(vm);
        }

        existedCategory.Name = vm.Name;

        if (vm.ParentCategoryId != null)
        {
            var isExistCategoryId = await _context.Categories.AnyAsync(x => x.Id == vm.ParentCategoryId && x.Id != vm.Id && x.ParentCategoryId == null);
            if (!isExistCategoryId)
            {
                ModelState.AddModelError("ParentCategoryId", "Böyle bir kategori bulunmamaktadır.");
                return View(vm);
            }

        }
        existedCategory.ParentCategoryId = vm.ParentCategoryId;



        if (vm.Image is not null)
        {
            var imageValidate = vm.Image.ValidateType();

            if (!imageValidate)
            {
                ModelState.AddModelError("Image", "Lütfen bir resim örneği seçiniz.");
                return View(vm);
            }

            imageValidate = vm.Image.ValidateSize(2);


            if (!imageValidate)
            {
                ModelState.AddModelError("Image", "Resmin maksimum kapasitesi 2 mb olmalı.");
                return View(vm);
            }

            var imagePath = await _cloudinaryService.FileCreateAsync(vm.Image);
            existedCategory.ImagePath = imagePath;

        }


        if (vm.BGImage is not null)
        {

            var bgImageValidate = vm.BGImage.ValidateType();

            if (!bgImageValidate)
            {
                ModelState.AddModelError("BGImage", "Lütfen bir resim örneği seçiniz.");
                return View(vm);
            }

            bgImageValidate = vm.BGImage.ValidateSize(2);


            if (!bgImageValidate)
            {
                ModelState.AddModelError("BGImage", "Resmin maksimum kapasitesi 2 mb olmalı.");
                return View(vm);
            }

            var bgImagePath = await _cloudinaryService.FileCreateAsync(vm.BGImage);

            existedCategory.BGImagePath = bgImagePath;

        }



        _context.Categories.Update(existedCategory);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.Include(x => x.ChildCategories).FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();


        if (category.ChildCategories.Count is not 0)
            return BadRequest();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Detail(int id)
    {
        var category = await _context.Categories.Include(x => x.ChildCategories).Include(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();


        return View(category);
    }
}
