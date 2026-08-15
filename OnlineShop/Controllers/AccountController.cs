using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using System.Xml.Linq;

namespace OnlineShop.Controllers
{
    public class AccountController(IUserService userService, ICartService cartService, IFavoriteService favoriteService, IComparatorService comparatorService, IOrderListService orderListService) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginData());
        }
        [HttpPost]
        public IActionResult Login(LoginData login)
        {
            (string name, string password, _) = login;

            if (!userService.GetAll().Any(user => user.Login == name))
                ModelState.AddModelError("Name", "USER_WITH_THIS_LOGIN_DOES_NOT_EXIST");

            if (userService.GetAll().FirstOrDefault(user => user.Login == name)?.Password != password)
                ModelState.AddModelError("Password", "INCORRECT_PASSWORD");

            if (!ModelState.IsValid)
                return View(login);

            LoginUser(name, password);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterData());
        }
        [HttpPost]
        public IActionResult Register(RegisterData register) 
        {
            (string name, string password, _) = register;

            if (userService.GetAll().Any(user => user.Login == name))
                ModelState.AddModelError("Name", "USER_WITH_THIS_LOGIN_IS_ACTUALY_EXIST");

            if (!ModelState.IsValid) 
                return View(register);

            RegisterUser(name, password);

            return RedirectToAction("Index", "Home");
        }
        private void RegisterUser(string name, string password) 
        {
            Cart cart = new Cart();
            Favorite favorite = new Favorite();
            OrderList orderList = new OrderList();
            Comparator comparator = new Comparator();

            User user = new User(name, password) 
            {
                CartId = cart.Id, 
                FavoriteId = favorite.Id, 
                OrderListId = orderList.Id, 
                ComparatorId = comparator.Id
            };

            cart.UserId = user.Id;
            favorite.UserId = user.Id;
            orderList.UserId = user.Id;
            comparator.UserId = user.Id;

            Info.Info.CommonUserId = user.Id;
            Info.Info.CommonFavoriteId = favorite.Id;
            Info.Info.CommonComparatorId = comparator.Id;
            Info.Info.CommonCartId = cart.Id;
            Info.Info.CommonOrderListId = orderList.Id;

            cartService.Update(cart);
            favoriteService.Update(favorite);
            comparatorService.Update(comparator);
            orderListService.Update(orderList);
            userService.Update(user);
        }
        private void LoginUser(string name, string password)
        {
            User user = userService.GetAll().First(user => user.Login == name);

            Cart cart = cartService.GetById(user.CartId) ?? new Cart() {UserId = user.Id};
            Favorite favorite = favoriteService.GetById(user.FavoriteId) ?? new Favorite() {UserId = user.Id};
            OrderList orderList = orderListService.GetById(user.OrderListId) ?? new OrderList() {UserId = user.Id};
            Comparator comparator = comparatorService.GetById(user.ComparatorId) ?? new Comparator() { UserId = user.Id};

            Info.Info.CommonUserId = user.Id;
            Info.Info.CommonFavoriteId = favorite.Id;
            Info.Info.CommonComparatorId = comparator.Id;
            Info.Info.CommonCartId = cart.Id;
            Info.Info.CommonOrderListId = orderList.Id;

            cartService.Update(cart);
            favoriteService.Update(favorite);
            comparatorService.Update(comparator);
            orderListService.Update(orderList);
            userService.Update(user);
        }
    }
}
