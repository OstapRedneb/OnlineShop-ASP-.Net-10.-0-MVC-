using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController(IProductService productService, ICartService cartService, IFavoriteService favoriteService, IComparatorService comparatorService, IOrderListService orderListService, IUserService userService, IRoleService roleService) : Controller
    {
        [HttpGet]
        public IActionResult Index(string searchString = "")
        {
            List<Product> products = productService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchString)) 
            {
                products = products.OrderByDescending(product => CountSearch(product.Name, searchString, 3)).ToList();
            }

            return View(products);
        }
        public IActionResult Initial() 
        {
            roleService.Clear();
            cartService.Clear();
            favoriteService.Clear();
            comparatorService.Clear();
            orderListService.Clear();
            userService.Clear();
            productService.Clear();
            productService.AddRange(
                [
                    new Product("CyberEyes", 99_999.99m),
                    new Product("SynthSlider", 20_199.99m),
                    new Product("HyperTimer", 10_000m)
                ]);

            Role userRole = new Role();
            Role adminRole = new Role()
            {
                Name = "Admin",
                CanAddProducts = true,
                CanChangeOrderStatus = true,
                CanChangeUserRoles = true,
                CanDeleteProducts = true,
                CanEditProducts = true,
                CanManageRoles = true,
                CanManageUsers = true,
                CanViewOrders = true,
            };

            roleService.AddRange(userRole, adminRole);

            Info.Info.CommonRoleId = userRole.Id;

            CreateAdmin();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int CountSearch(string name, string searchString, int splitDiapason = 3) 
        {
            name = name.ToUpper();
            searchString = searchString.ToUpper();
            
            if (string.IsNullOrWhiteSpace(searchString) || splitDiapason > name.Length || name.Length < searchString.Length)
                return 0;

            string concatSearch = string.Concat
                (
                    searchString.Split([' ', '\t', '\n', ',', '.', '/', '\\', ':', ';', '-'], 
                    StringSplitOptions.RemoveEmptyEntries)
                );

            int counter = 0;
            for (int i = 0; i < concatSearch.Length - splitDiapason; i++) 
            {
                if (name.Contains(string.Concat(concatSearch.Skip(i).Take(splitDiapason))))
                    counter++;
            }
            return counter;
        }

        private void CreateAdmin() 
        {
            User admin = new User("admin", "123456", "88005553535", "Dazdraperma", "Djugashvily", "admin@gmail.com");
            Cart cart = new Cart() { UserId = admin.Id };
            Favorite favorite = new Favorite() { UserId = admin.Id };
            OrderList orderList = new OrderList() { UserId = admin.Id };
            Comparator comparator = new Comparator() { UserId = admin.Id };

            admin.RoleId = roleService.GetByName("Admin").Id;
            admin.OrderListId = orderList.Id;
            admin.CartId = cart.Id;
            admin.ComparatorId = comparator.Id;
            admin.FavoriteId = favorite.Id;

            userService.Add(admin);
            cartService.Add(cart);
            favoriteService.Add(favorite);
            orderListService.Add(orderList);
            comparatorService.Add(comparator);
        }
    }
}
