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

        var query = _context.Blogs.AsQueryable();
        if (id is not null)
        {
            if (!await _context.BlogCategories.AnyAsync(x => x.Id == id))
                return NotFound();
            query = query.Where(x => x.BlogCategoryId == id);
        }
        var blogs=await query.ToListAsync();    
        var recentBlogs = await _context.Blogs.OrderByDescending(x=>x.Id).Take(2).ToListAsync();
        var categories = await _context.BlogCategories.Include(x=>x.Blogs).ToListAsync();

        BlogVM vm = new()
        { BlogCategories = categories, Blogs = blogs ,RecentBlogs=recentBlogs};


        return View(vm);
    }


    public async Task<IActionResult> Detail(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog is null)
            return NotFound();

        var categories = await _context.BlogCategories.Include(x => x.Blogs).ToListAsync();


        BlogDetailVM vm = new()
        {
            Blog = blog,
            BlogCategories = categories,
        };

        return View(vm);
    }
}


