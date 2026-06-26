using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Controllers
{
    public class OrderController(ICartService cartService) : Controller
    {
        public IActionResult Index()
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            return View(cart);
        }

        [HttpPost]
        public IActionResult Pay(Order order) 
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            //Clear
            cart.Clear();
            cartService.Update(cart);

            System.Console.WriteLine(order);

            //доделай сохранение заказов

            return RedirectToAction("Successfull");
        }
        public IActionResult Successfull() 
        {
            return View();
        }
    }
}
