using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController(IRoleService roleService) : Controller
    {
        public IActionResult Index()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            return View(roleService.GetAll());
        }
        public IActionResult Create()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            return View(new Role());
        }
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            if (
                    role.Name == "User" ||
                    role.Name == "Admin" ||
                    roleService.GetAll().Any(roleFromMemory => roleFromMemory.Name == role.Name)
               )
                ModelState.AddModelError("Name", "This error name is actualy exists");

            if (!ModelState.IsValid)
                return View(role);

            roleService.Add(role);

            return RedirectToAction("Index", "Role", "Admin");
        }
        [HttpPost]
        public IActionResult Delete(Guid roleId)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            Role role = roleService.GetById(roleId);

            if (roleId == Info.Info.CommonRoleId || role.Name == "User" || role.Name == "Admin")
                return RedirectToAction("Index", "Role", "Admin");

            roleService.Remove(role);
            return RedirectToAction("Index", "Role", "Admin");
        }
    }
}
