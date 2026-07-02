using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
    }
}
