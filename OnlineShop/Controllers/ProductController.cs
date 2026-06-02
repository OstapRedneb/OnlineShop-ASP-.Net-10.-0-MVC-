using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public string Index(Guid id)
        {
            List<Product> products = ProductService.GetAll();
            Product? product = products.FirstOrDefault(product => product.Id == id);

            string output = product is null ? "Нет такого продукта" : $"{product.Id}\n{product.Name}\n{product.Cost}";

            return output;
        }
    }
}
