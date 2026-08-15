using Newtonsoft.Json;
using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAll();
        public User? GetById(Guid id);
        public bool Add(User user);
        public void AddRange(params List<User> users);
        public bool Update(User user);
        public void Clear();
    }
}
