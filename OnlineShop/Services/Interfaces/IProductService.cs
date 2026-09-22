using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IProductService
    {
        public List<ProductViewModel> GetAll();
        public List<ProductViewModel> GetAllWithDeleted();
        public ProductViewModel? GetById(Guid id);
        public bool Add(ProductViewModel product);
        public void AddRange(params List<ProductViewModel> products);
        public bool Update(ProductViewModel product);
        public void Clear();
    }
}
