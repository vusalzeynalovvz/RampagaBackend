using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Areas.Admin.Utilities.Services;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;
    public BlogController(AppDbContext context, CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IActionResult> Index()
    {
        var blogs = await _context.Blogs.Include(x => x.BlogCategory).ToListAsync();
        return View(blogs);
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _context.BlogCategories.ToListAsync();
        ViewBag.Categories = categories;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BlogPostVM vm)
    {
        var categories = await _context.BlogCategories.ToListAsync();
        ViewBag.Categories = categories;

        if (!ModelState.IsValid)
            return View(vm);

        var isExistCategory = categories.Any(x => x.Id == vm.BlogCategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("BlogCategoryId", "Burda bir yanlışlık var.");
            return View(vm);
        }

        if (!vm.Image.ValidateImage(2))
        {
            ModelState.AddModelError("Image", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
            return View(vm);
        }

        Blog blog = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            BlogCategoryId = vm.BlogCategoryId,
            ImagePath = await _cloudinaryService.FileCreateAsync(vm.Image),
            CreatedTime = DateTime.UtcNow
        };


        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);

        if (blog is null)
            return NotFound();

        var categories = await _context.BlogCategories.ToListAsync();
        ViewBag.Categories = categories;


        BlogPutVM vm = new()
        {
            Id=id,
            Name=blog.Name,
            Description=blog.Description,
            ImagePath=blog.ImagePath,
            BlogCategoryId=blog.BlogCategoryId,

        };

        return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> Update(BlogPutVM vm)
    {

        var categories = await _context.BlogCategories.ToListAsync();
        ViewBag.Categories = categories;


        if (!ModelState.IsValid)
            return View(vm);

        var existBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (existBlog is null)
            return NotFound();

        var isExistCategory = categories.Any(x => x.Id == vm.BlogCategoryId);
        if (!isExistCategory)
        {
            ModelState.AddModelError("BlogCategoryId", "Burda bir yanlışlık var.");
            return View(vm);
        }

        if (vm.Image is not null && !vm.Image.ValidateImage(2))
        {
            ModelState.AddModelError("Image", "Resim doğru formatda ve boyutu 2 mb dan az olmalıdır");
            return View(vm);
        }


        existBlog.Name=vm.Name;
        existBlog.Description=vm.Description;
        existBlog.BlogCategoryId = vm.BlogCategoryId;

        if (vm.Image is not null)
            existBlog.ImagePath = await _cloudinaryService.FileCreateAsync(vm.Image);

        _context.Blogs.Update(existBlog);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Delete(int id)
    {
        var blog=await _context.Blogs.FirstOrDefaultAsync(x=>x.Id == id);

        if(blog is null)
            return NotFound();

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Detail(int id)
    {
        var blog = await _context.Blogs.Include(x=>x.BlogCategory).FirstOrDefaultAsync(x => x.Id == id);

        if (blog is null)
            return NotFound();

        return View(blog);
    }




}
