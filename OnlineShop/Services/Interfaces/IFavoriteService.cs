using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IFavoriteService
    {
        public List<Favorite> GetAll();
        public Favorite? GetById(Guid id);
        public bool Add(Favorite favorite);
        public void AddRange(params List<Favorite> favorites);
        public bool Update(Favorite favorite);
        public void Clear();
    }
}
