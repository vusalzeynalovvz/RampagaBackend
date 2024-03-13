using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.ViewModels;

namespace Rampage.Controllers;

public class ShopController : Controller
{
    private readonly AppDbContext _context;

    public ShopController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        var query = _context.Products.Include(x=>x.Category).AsQueryable();
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
        var product = await _context.Products.Include(x=>x.ProductInfos).Include(x=>x.ProductImages).Include(x=>x.Category).ThenInclude(x=>x.ParentCategory).FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();
        return View(product);
    }
}


