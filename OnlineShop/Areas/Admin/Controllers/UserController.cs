using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;
using System.Xml.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(IRoleService roleService, IUserService userService, ICartService cartService, IFavoriteService favoriteService, IComparatorService comparatorService, IOrderListService orderListService) : Controller
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
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            ViewBag.Roles = roleService.GetAll();

            return View(new UserCreate());
        }
        [HttpPost]
        public IActionResult Add(UserCreate userCreate) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            if (userService.GetAll().Any(user => user.Login == userCreate.Login))
                ModelState.AddModelError("Name", "USER_WITH_THIS_LOGIN_IS_ACTUALY_EXIST");

            if (userService.GetAll().Any(user => user.Email == userCreate.Email))
                ModelState.AddModelError("Email", "USER_WITH_THIS_EMAIL_IS_ACTUALY_EXIST");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = roleService.GetAll();
                return View(userCreate);
            }

            User user = (User)userCreate;
            
            Register(user);

            return RedirectToAction("Index", "User", "Admin");
        }
        public IActionResult ChangePassword(Guid id) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            return View(new ChangePassword {UserId = id});
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            User? user = userService.GetById(changePassword.UserId);

            if (user is null)
                return RedirectToAction("Index", "Home");

            if (changePassword.Password == user.Password)
                ModelState.AddModelError("Password", "passwords should be diferent");

            if (!ModelState.IsValid)
                return View(changePassword);

            user.Password = changePassword.Password;

            userService.Update(user);

            return RedirectToAction("Details", "User", new { id = user.Id });
        }
        public IActionResult ChangeRole(Guid id) 
        {
            if (!(roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false) || !(roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false))
                return RedirectToAction("Index", "Home");

            ViewBag.Roles = roleService.GetAll();

            return View(new ChangeRole { UserId = id });
        }
        [HttpPost]
        public IActionResult ChangeRole(ChangeRole changeRole) 
        {
            if (!(roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false) || !(roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false))
                return RedirectToAction("Index", "Home");

            User? user = userService.GetById(changeRole.UserId);

            if (user is null)
                return RedirectToAction("Index", "Home");

            if (user.RoleId == changeRole.RoleId)
                ModelState.AddModelError("RoleId", "User exactly has this role");

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = roleService.GetAll();
                return View(changeRole);
            }

            user.RoleId = changeRole.RoleId;

            userService.Update(user);

            return RedirectToAction("Details", "User", new {id =  user.Id});
        }
        public IActionResult Edit(Guid id) 
        {
            if (!(roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false))
                return RedirectToAction("Index", "Home");

            User? user = userService.GetById(id);

            if (user is null)
                return RedirectToAction("Index", "Home");

            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user) 
        {
            if (!(roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false) || user is null)
                return RedirectToAction("Index", "Home");

            if (userService.GetAll().Where(userInMemory => userInMemory.Login == user.Login).Count() > 1)
                ModelState.AddModelError("Login", "This login is actualy exists");

            if (!ModelState.IsValid)
                return View(user);

            userService.Update(user);

            return View("Details", "User");
        }

        private void Register(User user) 
        {
            Cart cart = new Cart();
            Favorite favorite = new Favorite();
            OrderList orderList = new OrderList();
            Comparator comparator = new Comparator();

            user.CartId = cart.Id;
            user.ComparatorId = comparator.Id;
            user.FavoriteId = favorite.Id;
            user.OrderListId = orderList.Id;

            cart.UserId = user.Id;
            favorite.UserId = user.Id;
            orderList.UserId = user.Id;
            comparator.UserId = user.Id;

            cartService.Update(cart);
            favoriteService.Update(favorite);
            comparatorService.Update(comparator);
            orderListService.Update(orderList);
            userService.Update(user);
        }
    }
}
