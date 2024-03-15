using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{
    private readonly AppDbContext _context;

    public SettingController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        var settings = await _context.Settings.ToListAsync();
        return View(settings);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SettingPostVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var isExist = await _context.Settings.AnyAsync(x => x.Key.ToLower() == vm.Key.ToLower());
        if (isExist)
        {
            ModelState.AddModelError("Key", "Bu Anahtar zaten mevcut");
            return View(vm);
        }

        Setting setting = new() { Key = vm.Key, Value = vm.Value };

        await _context.Settings.AddAsync(setting);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
        if (setting is null)
            return NotFound();

        SettingPutVM vm = new() { Id = id, Key = setting.Key, Value = setting.Value };

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(SettingPutVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var existSetting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == vm.Id);
        
        if(existSetting is null)
            return NotFound();


        existSetting.Value = vm.Value;

        _context.Settings.Update(existSetting);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }
}
