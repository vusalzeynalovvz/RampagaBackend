using Microsoft.AspNetCore.Mvc;

namespace Rampage.Controllers;

public class BasketController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}


