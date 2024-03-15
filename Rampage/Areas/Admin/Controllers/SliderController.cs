using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Areas.Admin.Utilities.Services;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly CloudinaryService _cloudinaryService;

    public SliderController(AppDbContext context, CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IActionResult> Index()
    {

        var sliders = await _context.Sliders.ToListAsync();
        return View(sliders);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SliderPostVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);


        if (!vm.Image.ValidateSize(2))
        {
            ModelState.AddModelError("Image", "Resim büyüklüğü 2 mb dan çok olamaz");
            return View(vm);
        }
        if (!vm.Image.ValidateType())
        {
            ModelState.AddModelError("Image", "Lütfen resim ekleyiniz");
            return View(vm);
        }




        Slider slider = new()
        {
            Name = vm.Name,
            ImagePath = await _cloudinaryService.FileCreateAsync(vm.Image),
            Subject = vm.Subject,
            Title = vm.Title,
            Button = vm.Button,
        };

        await _context.Sliders.AddAsync(slider);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

        if (slider is null)
            return NotFound();


        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

        if (slider is null)
            return NotFound();

        SliderPutVM vm = new()
        {
            Name = slider.Name,
            Subject = slider.Subject,
            Title = slider.Title,
            ImagePath = slider.ImagePath,
            Button = slider.Button,
            Id = id

        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(SliderPutVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var existSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == vm.Id);

        if (existSlider is null)
            return NotFound();

        if(vm.Image is not null)
        {
            if (!vm.Image.ValidateSize(2))
            {
                ModelState.AddModelError("Image", "Resim büyüklüğü 2 mb dan çok olamaz");
                return View(vm);
            }
            if (!vm.Image.ValidateType())
            {
                ModelState.AddModelError("Image", "Lütfen resim ekleyiniz");
                return View(vm);
            }

            existSlider.ImagePath=await _cloudinaryService.FileCreateAsync(vm.Image);   
        }


        existSlider.Subject = vm.Subject;
        existSlider.Title = vm.Title;
        existSlider.Button = vm.Button;
        existSlider.Name = vm.Name;


        _context.Sliders.Update(existSlider);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
