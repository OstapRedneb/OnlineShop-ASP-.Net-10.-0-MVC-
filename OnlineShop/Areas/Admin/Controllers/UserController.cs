using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IRoleService roleService, IUserService userService) : Controller
    {
        public IActionResult Index()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            return View(userService.GetAll());
        }
    }
}
