using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Controllers
{
    public class AdminController(IProductService productService, IOrderListService orderListService, IRoleService roleService) : Controller
    {
        public IActionResult Orders()
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
        public IActionResult OrderDetails(Guid orderId) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanChangeOrderStatus ?? false)
                return RedirectToAction("Index", "Home");

            return View(orderListService.GetAll().SelectMany(orderList => orderList.ToList()).FirstOrDefault(order => order.Id == orderId));
        }
        [HttpPost]
        public IActionResult UpdateOrderStatus(Guid id, OrderStatus status) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanChangeOrderStatus ?? false)
                return RedirectToAction("Index", "Home");

            OrderList orderList = orderListService
                                    .GetAll()
                                    .First(orderList => orderList.Any(orderInMemory => orderInMemory.Id == id));

            orderList.First(orderInMemory => orderInMemory.Id == id).Status = status;

            orderListService.Update(orderList);

            return RedirectToAction("Orders", "Admin");
        }
        public IActionResult User()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageUsers ?? false)
                return RedirectToAction("Index", "Home");

            return View();
        }
        public IActionResult Roles()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            return View(roleService.GetAll());
        }
        public IActionResult RoleCreate() 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            return View(new Role());
        }
        [HttpPost]
        public IActionResult RoleCreate(Role role) 
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

            return RedirectToAction("Roles", "Admin");
        }
        [HttpPost]
        public IActionResult RoleDelete(Guid roleId) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanManageRoles ?? false)
                return RedirectToAction("Index", "Home");

            Role role = roleService.GetById(roleId);

            if (roleId == Info.Info.CommonRoleId || role.Name == "User" || role.Name == "Admin")
                return RedirectToAction("Roles", "Admin");

            roleService.Remove(role);
            return RedirectToAction("Roles", "Admin");
        }
        public IActionResult Product()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.IsAdmin ?? false)
                return RedirectToAction("Index", "Home");

            List<Product> products = productService.GetAll();

            return View(products);
        }
        public IActionResult ProductCreate() 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanAddProducts ?? false)
                return RedirectToAction("Index", "Home");

            return View(new Product());
        }
        [HttpPost]
        public IActionResult ProductCreate(Product product) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanAddProducts ?? false)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(product);

            productService.Update(product);

            return RedirectToAction("Product");
        }
        public IActionResult ProductEdit(Guid id) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanEditProducts ?? false)
                return RedirectToAction("Index", "Home");

            Product? product = productService.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductEdit(Product product) 
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanEditProducts ?? false)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View(product);

            productService.Update(product);

            return RedirectToAction("Product");
        }
        public IActionResult ProductDelete(Guid id)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanDeleteProducts ?? false)
                return RedirectToAction("Index", "Home");

            Product? product = productService.GetById(id);

            if (product != null)
                product.IsDeleted = true;

            productService.Update(product);

            return RedirectToAction("Product");
        }
    }
}
