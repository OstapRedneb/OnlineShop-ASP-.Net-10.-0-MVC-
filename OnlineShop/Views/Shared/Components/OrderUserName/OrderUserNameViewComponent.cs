using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.OrderUserName
{
    public class OrderUserNameViewComponent(IUserService userService) : ViewComponent
    {
        public IViewComponentResult Invoke(Order order)
        {
            User? user = userService.GetById(order.UserId);

            return View("OrderUserName", user?.Login ?? "ERROR");
        }
    }
}
