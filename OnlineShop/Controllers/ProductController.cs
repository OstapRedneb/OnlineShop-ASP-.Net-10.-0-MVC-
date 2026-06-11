using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(Guid id)
        {
            Product? product = ProductService.GetById(id);

            if (product is null)
                return RedirectToAction("Index", "Home");

            return View(product);
        }
    }
}
