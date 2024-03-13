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

    public async Task<IActionResult> Index(int? categoryId)
    {
        var query = _context.Products.Include(x => x.Category).AsQueryable();
        ShopVM vm = new();
        if (categoryId is not null)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category is null)
                return NotFound();
            vm.Category = category;
            query = query.Where(x => x.Category.ParentCategoryId == categoryId);
        }
        var products = await query.ToListAsync();
        vm.Products = products;
        return View(vm);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var product = await _context.Products.Include(x => x.ProductInfos).Include(x => x.ProductImages).Include(x => x.Category).ThenInclude(x => x.ParentCategory).FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();
        return View(product);
    }
    [Authorize]
    public async Task<IActionResult> AddToBasket(int id, string? returnAction)
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

        if (userId is null)
            return Unauthorized();

        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null || product.Count <= 0)
            return NotFound();


        var existItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == userId);
        if (existItem is not null)
        {
            if (product.Count > existItem.Count)
                existItem.Count++;
            _context.BasketItems.Update(existItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            BasketItem item = new() { AppUserId = userId, ProductId = id, Count = 1 };

            await _context.BasketItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        if (returnAction is null)
            return RedirectToAction("Index");

        return RedirectToAction(returnAction);
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

        return RedirectToAction(returnAction);
    }
}


