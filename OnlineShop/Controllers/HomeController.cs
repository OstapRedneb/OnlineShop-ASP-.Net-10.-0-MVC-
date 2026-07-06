using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController(IProductService productService, ICartService cartService, IFavoriteService favoriteService, IOrderListService orderListService) : Controller
    {
        [HttpGet]
        public IActionResult Index(string searchString = "")
        {
            List<Product> products = productService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString)) 
            {
                products = products.OrderByDescending(product => CountSearch(product.Name, searchString, 3)).ToList();
            }

            return View(products);
        }
        public IActionResult Initial() 
        {
            cartService.Clear();
            favoriteService.Clear();
            orderListService.Clear();
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

        private int CountSearch(string name, string searchString, int splitDiapason = 3) 
        {
            if (string.IsNullOrWhiteSpace(searchString) || splitDiapason > name.Length || name.Length < searchString.Length)
                return 0;

            string concatSearch = string.Concat
                (
                    searchString.Split([' ', '\t', '\n', ',', '.', '/', '\\', ':', ';', '-'], 
                    StringSplitOptions.RemoveEmptyEntries)
                );

            int counter = 0;
            for (int i = 0; i < concatSearch.Length - splitDiapason; i++) 
            {
                if (name.Contains(string.Concat(concatSearch.Skip(i).Take(splitDiapason))))
                    counter++;
            }
            return counter;
        }
    }
}
