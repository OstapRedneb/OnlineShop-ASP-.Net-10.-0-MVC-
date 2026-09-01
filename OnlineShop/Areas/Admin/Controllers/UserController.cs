using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
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
        public IActionResult Details(Guid id) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            User? user = userService.GetById(id);

            if (user is null)
                return RedirectToAction("Index", "Home");

            return View(user);
        }
        public IActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(UserCreate userCreate) 
        {

        }
    }
}
