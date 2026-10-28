using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
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
            }

            return View((CartViewModel)cart);
        }
        public IActionResult Add(Guid productId)
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);
            Product? product = productService.GetById(productId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
            }

            cart.Positions.Add(new Position() {Product = product});

            cartService.Update(cart);

            return RedirectToAction("Index");
        }
        public IActionResult Update(Guid positionId, string change)
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
            }

            Position? position = cart.Positions?.FirstOrDefault(position => position.Id == positionId);

            if (position != null)
            {
                if (change == "incr")
                    position.Quantity++;

                else if (position.Quantity <= 1)
                    cart.Positions?.Remove(position);

                else 
                    position.Quantity--;

                cartService.Update(cart);
            }

            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
            {
                cart = new Cart() { Id = Info.Info.CommonCartId };
            }

            cart.Positions.Clear();
            cartService.Update(cart);

            return RedirectToAction("Index");
        }
    }
}
