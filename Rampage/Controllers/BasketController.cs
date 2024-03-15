using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;
using Stripe;
using System.Security.Claims;

namespace Rampage.Controllers;

public class BasketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    public BasketController(AppDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var basketItems = await _context.BasketItems.Where(x => x.AppUserId == userId && x.IsSale == false).Include(x => x.Product).ToListAsync();

        return View(basketItems);
    }
    [Authorize]
    public async Task<IActionResult> Order()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized();

        var basketItems = await _context.BasketItems.Where(x => x.AppUserId == userId && x.IsSale == false).Include(x => x.Product).ToListAsync();

        decimal total = 0;

        basketItems.ForEach(x =>
        {
            total += x.Product.Price * x.Count;
        });


        ViewBag.Total = total;

        return View(); ;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Order(OrderVM vm,string stripeToken)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return BadRequest();




        var basketItems = await _context.BasketItems.Where(x => x.AppUserId == userId && x.IsSale == false).Include(x => x.Product).ToListAsync();
        decimal total = 0;

        basketItems.ForEach(x =>
        {
            total += x.Product.Price * x.Count;
        });


        ViewBag.Total = total;


        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        Order order = new()
        {
            Address = vm.Address,
            City = vm.City,
            FullName = vm.FullName,
            AppUserId = userId,
            PhoneNumber = vm.PhoneNumber,
            CreatedTime = DateTime.Now,
            TotalPrice = total
        };

        basketItems.ForEach((x) =>
        {
            x.IsSale = true;
            x.Order = order;
            x.StaticPrice = x.Product.Price;
            x.Product.SalesCount += x.Count;
            x.Product.Count -= x.Count;
            _context.Products.Update(x.Product);
            _context.BasketItems.Update(x);

        });

        var user = await _userManager.FindByIdAsync(userId);

        var optionCust = new CustomerCreateOptions
        {
            Email = user.Email,
            Name = user.FullName,
            Phone = vm.PhoneNumber
        };
        var serviceCust = new CustomerService();
        Customer customer = serviceCust.Create(optionCust);

        total = total * 100;
        var optionsCharge = new ChargeCreateOptions
        {

            Amount = (long)total,
            Currency = "USD",
            Description = "Product Selling amount",
            Source = stripeToken,
            ReceiptEmail = user.Email


        };
        var serviceCharge = new ChargeService();
        Charge charge = serviceCharge.Create(optionsCharge);



        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();






        return RedirectToAction("Index", "Shop");

    }
}



