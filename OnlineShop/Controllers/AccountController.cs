using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginData());
        }
        [HttpPost]
        public IActionResult Login(LoginData login)
        {
            if (!ModelState.IsValid)
                return View(login);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterData());
        }
        [HttpPost]
        public IActionResult Register(RegisterData register) 
        {
            if (!ModelState.IsValid) 
                return View(register);

            return RedirectToAction("Index", "Home");
        }
    }
}
