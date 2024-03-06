using Microsoft.AspNetCore.Mvc;

namespace Rampage.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}


