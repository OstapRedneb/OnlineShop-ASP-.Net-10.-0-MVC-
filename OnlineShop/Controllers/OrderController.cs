using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;

namespace OnlineShop.Controllers
{
    public class OrderController(ICartService cartService, IOrderListService orderListService) : Controller
    {
        public IActionResult Index()
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            return View(cart);
        }

        [HttpPost]
        public IActionResult Pay(Order order) 
        {
            Cart? cart = cartService.GetById(Info.Info.CommonCartId);
            OrderList? orderList = orderListService.GetById(Info.Info.CommonOrderListId);

            if (cart is null)
                return RedirectToAction("Index", "Cart");

            if (orderList is null) 
            {
                OrderList newOrderList = new OrderList(Info.Info.CommonOrderListId, new List<Order>());
                orderListService.Add(newOrderList);
                orderList = newOrderList;
            }

            //Clear
            cart.Clear();
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
