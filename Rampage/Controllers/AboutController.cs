using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;

namespace Rampage.Controllers;

public class AboutController : Controller
{
    private readonly AppDbContext _context;
public AboutController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var comments = await _context.Comments.OrderByDescending(x => x.Rating).Take(3).Include(x => x.Product).Include(x => x.AppUser).ToListAsync();

        return View(comments);
    }
}


