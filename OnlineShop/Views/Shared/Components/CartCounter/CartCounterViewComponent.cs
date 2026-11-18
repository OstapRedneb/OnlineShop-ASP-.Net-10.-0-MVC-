using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.CartCounter
{
    public class CartCounterViewComponent(ICartService cartService) : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            decimal answer = cart is null ? 0 : cart.Positions.Sum(position => position.Quantity);

            return View("CartCounter", answer);
        }
    }
}
