using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.AdminPanel
{
    public class AdminPanelViewComponent(IRoleService roleService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Role? role = roleService.GetById(Info.Info.CommonRoleId);

            return View("AdminPanel", role?.IsAdmin ?? false);
        }
    }
}
