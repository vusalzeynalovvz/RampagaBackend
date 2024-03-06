using Microsoft.AspNetCore.Mvc;

namespace Rampage.Controllers;

public class ShopController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Detail()
    {
        return View();
    }
}


