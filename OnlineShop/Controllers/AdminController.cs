using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class AdminController(IProductService productService) : Controller
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
            List<Product> products = productService.GetAll();

            return View(products);
        }
        public IActionResult ProductCreate() 
        {
            return View();
        }
        public IActionResult ProductEdit(Guid id) 
        {
            Product? product = productService.GetById(id);
            return View(product);
        }
        public IActionResult ProductDelete(Guid id)
        {
            Product? product = productService.GetById(id);

            if (product != null)
                product.IsDeleted = true;

            productService.Update(product);

            return RedirectToAction("Product");
        }
        [HttpPost]
        public IActionResult ProductSave(Product product)
        {
            productService.Update(product);

            return RedirectToAction("Product");
        }
    }
}
