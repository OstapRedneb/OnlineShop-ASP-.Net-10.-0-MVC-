using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Views.Shared.Components.UserName
{
    public class UserNameViewComponent(IUserService userService) : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            User? user = userService.GetById(Info.Info.CommonUserId);

            return View("UserName", user?.Login ?? "UNKNOW");
        }
    }
}
