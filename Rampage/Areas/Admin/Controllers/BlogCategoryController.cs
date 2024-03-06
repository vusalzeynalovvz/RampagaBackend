using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogCategoryController : Controller
{
    private readonly AppDbContext _context;

    public BlogCategoryController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categoires = await _context.BlogCategories.Include(x => x.Blogs).ToListAsync();
        return View(categoires);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BlogCategoryPostVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var isExist = await _context.BlogCategories.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower());

        if (isExist)
        {
            ModelState.AddModelError("Name", "Bu kategori zaten mevcut");
            return View(vm);
        }

        BlogCategory category = new() { Name = vm.Name };


        await _context.BlogCategories.AddAsync(category);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.BlogCategories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return NotFound();

        BlogCategoryPutVM vm = new() { Id = id, Name = category.Name };


        return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> Update(BlogCategoryPutVM vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var existCategory = await _context.BlogCategories.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (existCategory is null)
            return NotFound();

        var isExist=await _context.BlogCategories.AnyAsync(x=>x.Name.ToLower()==vm.Name.ToLower() && x.Id!=vm.Id);

        if (isExist)
        {
            ModelState.AddModelError("Name", "Bu kategori zaten mevcut");
            return View(vm);
        }


        existCategory.Name=vm.Name;

        _context.BlogCategories.Update(existCategory);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.BlogCategories.FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
            return NotFound();


        _context.BlogCategories.Remove(category);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Detail (int id)
    {
        var category = await _context.BlogCategories.Include(x=>x.Blogs).FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

}
