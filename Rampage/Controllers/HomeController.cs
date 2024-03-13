using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.ViewModels;

namespace Rampage.Controllers;

public class HomeController : Controller
{

    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HomeVM vm = new()
        {
            NewProducts = await _context.Products.OrderByDescending(x => x.Id).Take(2).ToListAsync(),
            PopularProducts = await _context.Products.OrderBy(x => x.Price).Take(2).ToListAsync(),
            SpecialProducts = await _context.Products.OrderByDescending(x => x.Price).Take(2).ToListAsync(),

            Categories = await _context.Categories.Take(4).ToListAsync(),
            SpecialCategories=await _context.Categories.Include(x=>x.Products).OrderByDescending(x => x.Products.Count).Take(4).ToListAsync()
        };

        return View(vm);
    }

}


