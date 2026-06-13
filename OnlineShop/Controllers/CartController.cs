using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class CartController(IProductService productService, ICartService cartService) : Controller
    {
        public IActionResult Index()
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                cartService.Add(cart);
            }

            return View(cart);
        }
        public IActionResult Add(Guid productId)
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);
            Product? product = productService.GetById(productId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                cartService.Add(cart);
            }

            cart.Add(product);

            cartService.Update(cart);

            return RedirectToAction("Index");
        }
        public IActionResult Clear() 
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
                cartService.Add(cart);
            }
            
            cart.Clear();
            cartService.Update(cart);

            return RedirectToAction("Index");
        }
    }
}
