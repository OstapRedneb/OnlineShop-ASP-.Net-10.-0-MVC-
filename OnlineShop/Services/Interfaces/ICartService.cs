using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface ICartService
    {
        public List<CartViewModel> GetAll();
        public CartViewModel? GetById(Guid id);
        public bool Add(CartViewModel cart);
        public void AddRange(params List<CartViewModel> carts);
        public bool Update(CartViewModel cart);
        public void Clear();
    }
}
