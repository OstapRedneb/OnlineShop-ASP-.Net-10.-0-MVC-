using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public string Index(Guid id)
        {
            Product? product = ProductService.GetById(id);

            string output = product is null ? "Нет такого продукта" : $"{product.Id}\n{product.Name}\n{product.Cost}";

            return output;
        }
    }
}
