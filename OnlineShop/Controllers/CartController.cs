using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            Cart? cart = CartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                CartService.Add(cart);
            }

            return View(cart);
        }
        public IActionResult Add(Guid productId)
        {
            Cart? cart = CartService.GetById(Info.Info.CommonCartId);
            Product? product = ProductService.GetById(productId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                CartService.Add(cart);
            }

            cart.Add(product);

            CartService.Update(cart);

            return RedirectToAction("Index");
        }
        public IActionResult Clear() 
        {
            Cart? cart = CartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                CartService.Add(cart);
            }
            
            cart.Clear();
            CartService.Update(cart);

            return RedirectToAction("Index");
        }
    }
}
