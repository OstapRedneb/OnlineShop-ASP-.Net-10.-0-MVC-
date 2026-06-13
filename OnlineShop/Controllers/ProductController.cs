using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        public IActionResult Index(Guid id)
        {
            Product? product = productService.GetById(id);

            if (product is null)
                return RedirectToAction("Index", "Home");

            return View(product);
        }
    }
}
