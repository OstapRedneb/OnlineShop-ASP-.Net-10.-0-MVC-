using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController(IProductService productService, ICartService cartService) : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = productService.GetAll();

            return View(products);
        }
        public IActionResult Initial() 
        {
            cartService.Clear();
            productService.Clear();
            productService.AddRange(
                [
                    new Product("CyberEyes", 99_999.99m),
                    new Product("SynthSlider", 20_199.99m),
                    new Product("HyperTimer", 10_000m)
                ]);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
