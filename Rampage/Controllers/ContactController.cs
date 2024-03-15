using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Macs;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Database;

namespace Rampage.Controllers;

public class ContactController : Controller
{
    private readonly AppDbContext _context;
    private readonly MailKitHelper _mailKitHelper;



    public ContactController(AppDbContext context, MailKitHelper mailKitHelper)
    {
        _context = context;
        _mailKitHelper = mailKitHelper;
    }

    public async Task<IActionResult> Index()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
        return View(settings);
    }

    public async Task<IActionResult> Contact(string fullname, string email, string message)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Index");


        await _mailKitHelper.SendEmailAsync(new()
        {
            ToEmail = "rampagehelpp@gmail.com",
            Body = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n    <style>\r\n        body {{\r\n            color: black;\r\n            background-color: white;\r\n        }}\r\n\r\n        table {{\r\n            border-collapse: collapse;\r\n            width: 100%;\r\n        }}\r\n\r\n        th, td {{\r\n            border: 1px solid black;\r\n            padding: 8px;\r\n            text-align: left;\r\n        }}\r\n\r\n        th {{\r\n            background-color: red;\r\n            color: white;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n<table>\r\n    <thead>\r\n        <th>Ad Soyad</th>\r\n        <th>Email</th>\r\n        <th>Mesaj</th>\r\n    </thead>\r\n    <tbody>\r\n        <tr>\r\n            <td>{fullname}</td>\r\n            <td>{email}</td>\r\n            <td>{message}</td>\r\n        </tr>\r\n    </tbody>\r\n</table>    \r\n</body>\r\n</html>\r\n",
            Subject="Website Contact"

        }) ;

        return RedirectToAction("Index");


    }
}


