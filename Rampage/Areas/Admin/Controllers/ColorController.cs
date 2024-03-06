using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
public class ColorController : Controller
{
    private readonly AppDbContext _context;

    public ColorController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var colors = await _context.Colors.Include(x => x.Products).ToListAsync();
        return View(colors);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ColorPostVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var isExist = await _context.Colors.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Name", "Bu renk zaten mevcut.");
            return View(vm);
        }

        Color color = new() { Name = vm.Name };

        await _context.Colors.AddAsync(color);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
        if (color is null)
            return NotFound();

        ColorPutVM vm = new() { Id = id, Name = color.Name };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ColorPutVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var existed = await _context.Colors.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (existed is null)
            return BadRequest();


        var isExist=await _context.Colors.AnyAsync(x=>x.Name.ToLower()==vm.Name.ToLower() && x.Id!=vm.Id);
        if (isExist)
        {

            ModelState.AddModelError("Name", "Bu renk zaten mevcut.");
            return View(vm);
        }


        existed.Name = vm.Name;

        _context.Colors.Update(existed);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Delete(int id)
    {
        var color = await _context.Colors.Include(x=>x.Products).FirstOrDefaultAsync(x => x.Id == id);

        if(color is null)
            return NotFound();

        if (color.Products.Count > 0)
            return BadRequest();

        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Detail(int id)
    {
        var color = await _context.Colors.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

        if (color is null)
            return NotFound();

        return View(color);

    }
}
