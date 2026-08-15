using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class AdminController(IProductService productService, IOrderListService orderListService) : Controller
    {
        public IActionResult Orders()
        {
            return View
                (
                    orderListService
                        .GetAll()
                        .SelectMany(orderList => orderList.ToList())
                        .ToList()
                );
        }
        public IActionResult OrderDetails(Guid orderId) 
        {
            return View(orderListService.GetAll().SelectMany(orderList => orderList.ToList()).FirstOrDefault(order => order.Id == orderId));
        }
        [HttpPost]
        public IActionResult UpdateOrderStatus(Guid id, OrderStatus status) 
        {
            OrderList orderList = orderListService
                                    .GetAll()
                                    .First(orderList => orderList.Any(orderInMemory => orderInMemory.Id == id));

            orderList.First(orderInMemory => orderInMemory.Id == id).Status = status;

            orderListService.Update(orderList);

            return RedirectToAction("Orders", "Admin");
        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public IActionResult Product()
        {
            List<Product> products = productService.GetAll();

            return View(products);
        }
        public IActionResult ProductCreate() 
        {
            return View(new Product());
        }
        [HttpPost]
        public IActionResult ProductCreate(Product product) 
        {
            if (!ModelState.IsValid)
                return View(product);

            productService.Update(product);

            return RedirectToAction("Product");
        }
        public IActionResult ProductEdit(Guid id) 
        {
            Product? product = productService.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductEdit(Product product) 
        {
            if (!ModelState.IsValid)
                return View(product);

            productService.Update(product);

            return RedirectToAction("Product");
        }
        public IActionResult ProductDelete(Guid id)
        {
            Product? product = productService.GetById(id);

            if (product != null)
                product.IsDeleted = true;

            productService.Update(product);

            return RedirectToAction("Product");
        }
    }
}
