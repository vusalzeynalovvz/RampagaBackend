using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Database.DomainModels;
using Rampage.ViewModels;

namespace Rampage.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly MailKitHelper _mailKitHelper;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, MailKitHelper mailKitHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _mailKitHelper = mailKitHelper;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);


        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Email ve ya şifre yanlış.");
            return View(vm);
        }

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false,true);
            
        if (!result.Succeeded)
        {
            if(!user.EmailConfirmed)
                ModelState.AddModelError("", "E-posta adresini onayladıktan sonra giriş yapa bilirsiniz");
            else if(result.IsLockedOut)
                ModelState.AddModelError("", "User engellendi 5 dakika sonra yeniden deneyin");
            else
                ModelState.AddModelError("", "Email ve ya şifre yanlış.");
            return View(vm);
        }

        return RedirectToAction("Index","home");
    }

    public IActionResult UserInfo()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        AppUser user = new()
        {
            UserName = Guid.NewGuid().ToString(),
            FullName = vm.FullName,
            Email = vm.Email,
        };


        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }


        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);


        string? url = Url.Action("ActivateUser", "Account", new { userId = user.Id, token }, HttpContext.Request.Scheme);

        string body = messageBody.Replace("REPLACE_URL", url);



        await _mailKitHelper.SendEmailAsync(new() { ToEmail = user.Email, Subject = "Rampage Hesap Doğrulama", Body = body });


        return RedirectToAction("Index", "Home");
    }





    public async Task<IActionResult> ActivateUser(string userId, string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return BadRequest();

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return BadRequest();

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        return BadRequest();
    }







    string messageBody = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <link rel=\"preconnect\" href=\"https://fonts.googleapis.com\">\r\n    <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin>\r\n    <link href=\"https://fonts.googleapis.com/css2?family=Madimi+One&display=swap\" rel=\"stylesheet\">\r\n    <title>Document</title>\r\n    <style>\r\n        * {\r\n            margin: 0;\r\n            padding: 0;\r\n            box-sizing: border-box;\r\n            text-align: center;\r\n        }\r\n\r\n        .email-button {\r\n            position: absolute;\r\n            top: 50%;\r\n            left: 50%;\r\n            transform: translate(-50%, -50%);\r\n        }\r\n\r\n        h3 {\r\n            font-family: \"Madimi One\", sans-serif;\r\n            font-weight: 400;\r\n            font-style: normal;\r\n            font-size: 35px;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n        a {\r\n            color: white;\r\n            text-decoration: none;\r\n            cursor: pointer;\r\n        }\r\n\r\n        button {\r\n            margin-top: 20px;\r\n            padding: 20px 35px;\r\n            border: 1px solid red;\r\n            background-color: red;\r\n            color: #ffff;\r\n            font-family: \"Madimi One\", sans-serif;\r\n            cursor: pointer;\r\n            display: inline-block;\r\n            text-decoration: none;\r\n        }\r\n    </style>\r\n</head>\r\n\r\n<body>\r\n    <div class=\"email-button\">\r\n        <h3>E-posta adresinizi onaylamak için aşağıdaki düğmeye tıklayın Rampage.com.tr</h3>\r\n        <a href=\"REPLACE_URL\" style=\"color: white; text-decoration: none;\">\r\n            <button>E-posta adresinizi onayla</button>\r\n        </a>\r\n    </div>\r\n</body>\r\n\r\n</html>\r\n";
}


