using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using System.Security.Claims;

namespace Rampage.Controllers;

public class BasketController : Controller
{
    private readonly AppDbContext _context;

    public BasketController(AppDbContext context)
    {
        _context = context;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var basketItems = await _context.BasketItems.Where(x => x.AppUserId == userId).Include(x=>x.Product).ToListAsync();

        return View(basketItems);
    }
}


