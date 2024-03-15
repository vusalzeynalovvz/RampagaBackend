using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;
using System.Security.Claims;

namespace Rampage.Controllers;

public class ShopController : Controller
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _context;

    public ShopController(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<IActionResult> Index(int? categoryId, int? colorId)
    {

        ViewBag.CategoryId = categoryId;
        ViewBag.ColorId = colorId;
        var query = _context.Products.Include(x => x.Category).AsQueryable();
        ShopVM vm = new();
        if (categoryId is not null)
        {
            var category = await _context.Categories.Include(x => x.ParentCategory).FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category is null)
                return NotFound();
            vm.Category = category;

            if (category.ParentCategoryId is not null)
            {
                vm.ChildCategory = category;
                vm.Category = category.ParentCategory;
                query = query.Where(x => x.CategoryId == categoryId);
            }
            else
                query = query.Where(x => x.Category.ParentCategoryId == categoryId);
        }
        if (colorId is not null)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == colorId);
            if (color is null)
                return NotFound();

            vm.Color = color;
            query = query.Where(x => x.ColorId == colorId);
        }
        var products = await query.ToListAsync();
        vm.Products = products;
        vm.Categories = await _context.Categories.Where(x => x.ParentCategoryId == null).Include(x => x.ChildCategories).ToListAsync();
        vm.Colors = await _context.Colors.ToListAsync();


        if (vm.Category is not null)
            vm.Categories.Remove(vm.Category);
        return View(vm);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products.Include(x => x.ProductInfos).Include(x => x.ProductImages).Include(x => x.Comments).ThenInclude(x => x.AppUser).Include(x => x.Category).ThenInclude(x => x.ParentCategory).FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();
        return View(product);
    }
    [Authorize]
    public async Task<IActionResult> AddToBasket(int id, int? count, string? returnAction)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

        if (userId is null)
            return Unauthorized();

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null || product.Count <= 0)
            return NotFound();


        var existItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == userId && x.IsSale==false);
        if (existItem is not null)
        {
            if (product.Count > existItem.Count)
            {
                if (count is null)
                    existItem.Count++;
                else
                    existItem.Count += (int)count;
            }
            _context.BasketItems.Update(existItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            BasketItem item = new() { AppUserId = userId, ProductId = id, Count = 1 };

            if (count is not null)
                item.Count = (int)count;
            await _context.BasketItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        if (returnAction is null)
            return RedirectToAction("Index");

        return Redirect(returnAction);
    }

    [Authorize]
    public async Task<IActionResult> Decrease(int id, string? returnAction)
    {

        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

        if (userId is null)
            return Unauthorized();

        var bItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == userId);

        if (bItem is null)
            return NotFound();

        if (bItem.Count > 1)
            bItem.Count--;

        _context.BasketItems.Update(bItem);
        await _context.SaveChangesAsync();

        if (returnAction is null)
            return RedirectToAction("Index");

        return Redirect(returnAction);
    }


    [Authorize]
    public async Task<IActionResult> RemoveBasketItem(int id, string? returnAction)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var bItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.Id == id && x.AppUserId == userId);

        if (bItem is null)
            return NotFound();


        _context.Remove(bItem);
        await _context.SaveChangesAsync();

        if (returnAction is null)
            return RedirectToAction("Index");

        return Redirect(returnAction);
    }

    [Authorize]
    public async Task<IActionResult> ClearBasket()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var bItems = await _context.BasketItems.Where(x => x.AppUserId == userId).ToListAsync();


        _context.BasketItems.RemoveRange(bItems);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Basket");
    }

    [Authorize]
    public async Task<IActionResult> PostComment(int productId, int rating, string comment)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return BadRequest();

        var product = await _context.Products.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            return NotFound();

        Comment cm = new()
        {
            AppUserId = userId,
            ProductId = productId,
            Rating = rating,
            Title = comment,
            CreatedTime = DateTime.Now,

        };



        product.Comments.Add(cm);

        decimal total = 0;

        foreach (var item in product.Comments)
        {
            total += item.Rating;
        }


        total = Math.Ceiling(total / product.Comments.Count);


        product.Rating = (int)total;

        _context.Products.Update(product);

        await _context.Comments.AddAsync(cm);
        await _context.SaveChangesAsync();

        return RedirectToAction("Detail", new { id = productId });
    }
}


