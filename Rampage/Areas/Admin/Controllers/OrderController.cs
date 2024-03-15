using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{

    private readonly AppDbContext _appDbContext;

    public OrderController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _appDbContext.Orders.ToListAsync();
        return View(orders);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var order = await _appDbContext.Orders.Include(x=>x.BasketItems).ThenInclude(x=>x.Product).Include(x=>x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            return NotFound();


        return View(order);
    }
}
