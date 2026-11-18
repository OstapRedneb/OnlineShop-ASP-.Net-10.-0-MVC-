using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IProductService productService, IRoleService roleService) : Controller
    {
        public IActionResult Index()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.IsAdmin ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            List<Product> products = productService.GetAll();

            return View(products.Select(product => (ProductViewModel)product));
        }
        public IActionResult Create()
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanAddProducts ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            return View(new ProductViewModel());
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanAddProducts ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            if (!ModelState.IsValid)
                return View(product);

            productService.Add((Product)product);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanEditProducts ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            Product? product = productService.GetById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanEditProducts ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            if (!ModelState.IsValid)
                return View(product);

            productService.Update((Product)product);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (!roleService.GetById(Info.Info.CommonRoleId)?.CanDeleteProducts ?? false)
                return RedirectToAction("Index", "Home", new {area = ""});

            Product? product = productService.GetById(id);

            if (product != null)
                product.IsDeleted = true;

            productService.Update(product);

            return RedirectToAction("Index");
        }
    }
}
