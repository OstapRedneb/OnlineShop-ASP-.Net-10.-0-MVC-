using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IFavoriteService
    {
        public List<Favorite> GetAll();
        public Favorite? GetById(Guid id);
        public bool Add(Favorite cart);
        public void AddRange(params List<Favorite> carts);
        public bool Update(Favorite cart);
        public void Clear();
    }
}
