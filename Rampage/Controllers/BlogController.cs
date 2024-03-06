using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.ViewModels;

namespace Rampage.Controllers;

public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)
    {

        var blogs = await _context.Blogs.ToListAsync();
        var categories = await _context.BlogCategories.ToListAsync();

        BlogVM vm = new()
        { BlogCategories = categories, Blogs = blogs };


        return View(vm);
    }
}


