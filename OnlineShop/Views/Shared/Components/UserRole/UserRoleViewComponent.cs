using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Views.Shared.Components.UserRole
{
    public class UserRoleViewComponent(IRoleService roleService) : ViewComponent
    {
        public IViewComponentResult Invoke(Guid roleId)
        {
            Role? role = roleService.GetById(roleId);

            return View("UserRole", role?.Name ?? "User");
        }
    }
}
