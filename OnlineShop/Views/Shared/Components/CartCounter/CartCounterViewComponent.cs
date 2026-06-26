using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.CartCounter
{
    public class CartCounterViewComponent(ICartService cartService) : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            int answer = cart is null ? 0 : cart.Sum(position => position.Quantity);

            return View("CartCounter", answer);
        }
    }
}
