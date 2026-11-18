using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using System.Runtime.InteropServices;

namespace OnlineShop.Controllers
{
    public class OrderController(ICartService cartService, IOrderListService orderListService, IUserService userService) : Controller
    {
        public IActionResult Index()
        {
            if (userService.GetById(Info.Info.CommonUserId) is null)
                return RedirectToAction("Register", "Account");

            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            return View(new Order(((CartViewModel)cart).ToList()));
        }

        [HttpPost]
        public IActionResult Index(Order order) 
        {
            if (userService.GetById(Info.Info.CommonUserId) is null)
                return RedirectToAction("Register", "Account");

            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (!ModelState.IsValid)
                return View(order with { Positions = cart.Positions.Select(pos => (PositionViewModel)pos).ToList() });

            OrderList? orderList = orderListService.GetById(Info.Info.CommonOrderListId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            if (orderList is null) 
            {
                OrderList newOrderList = new OrderList(Info.Info.CommonOrderListId, new List<Order>());
                orderListService.Add(newOrderList);
                orderList = newOrderList;
            }

            order = order with { Positions= cart.Positions.Select(pos => (PositionViewModel)pos).ToList() };
            order.UserId = Info.Info.CommonUserId;

            //Clear
            cart.Positions.Clear();
            cartService.Update(cart);

            orderList.Add(order);

            orderListService.Update(orderList);

            return RedirectToAction("Successfull");
        }
        public IActionResult Successfull() 
        {
            return View();
        }
    }
}
