using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController(IOrderListService orderListService, IRoleService roleService) : Controller
    {
        public IActionResult Index()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanViewOrders ?? false)
                return RedirectToAction("Index", "Home");

            return View
                (
                    orderListService
                        .GetAll()
                        .SelectMany(orderList => orderList.ToList())
                        .ToList()
                );
        }
        public IActionResult Details(Guid orderId)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanChangeOrderStatus ?? false)
                return RedirectToAction("Index", "Home");

            return View(orderListService.GetAll().SelectMany(orderList => orderList.ToList()).FirstOrDefault(order => order.Id == orderId));
        }
        [HttpPost]
        public IActionResult UpdateStatus(Guid id, OrderStatus status)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanChangeOrderStatus ?? false)
                return RedirectToAction("Index", "Home");

            OrderList orderList = orderListService
                                    .GetAll()
                                    .First(orderList => orderList.Any(orderInMemory => orderInMemory.Id == id));

            orderList.First(orderInMemory => orderInMemory.Id == id).Status = status;

            orderListService.Update(orderList);

            return RedirectToAction("Index", "Order", "Admin");
        }
    }
}
