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
            SpecialCategories = await _context.Categories.Where(x => x.ParentCategoryId != null).Include(x => x.Products).OrderByDescending(x => x.Products.Count).Take(4).ToListAsync()
        };

        return View(vm);
    }


    public async Task<IActionResult> Subscribe(string email, string? returnAction)
    {
        var exist = await _context.Subscribes.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

        if (exist is null)
            await _context.Subscribes.AddAsync(new() { Email = email });
        else
            _context.Remove(exist);

        await _context.SaveChangesAsync();

        if (returnAction is null)
            return RedirectToAction("Index");


        return Redirect(returnAction);

    }

}


