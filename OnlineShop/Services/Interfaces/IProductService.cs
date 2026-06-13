using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetAll();
        public Product? GetById(Guid id);
        public bool Add(Product product);
        public void AddRange(params List<Product> products);
        public void Clear();
    }
}
