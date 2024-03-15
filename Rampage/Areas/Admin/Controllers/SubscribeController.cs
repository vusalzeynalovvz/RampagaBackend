using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SubscribeController : Controller
{
    private readonly AppDbContext _context;

    public SubscribeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var subscribes = await _context.Subscribes.ToListAsync();
        return View(subscribes);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var subscribe = await _context.Subscribes.FirstOrDefaultAsync(x => x.Id == id);

        if (subscribe is null)
            return NotFound();

        _context.Subscribes.Remove(subscribe); 
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
