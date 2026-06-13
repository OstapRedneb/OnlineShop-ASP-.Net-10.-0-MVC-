using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface ICartService
    {
        public List<Cart> GetAll();
        public Cart? GetById(Guid id);
        public bool Add(Cart cart);
        public void AddRange(params List<Cart> carts);
        public bool Update(Cart cart);
        public void Clear();
    }
}
