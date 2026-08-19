using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IRoleService
    {
        public List<Role> GetAll();
        public Role? GetById(Guid id);
        public Role? GetByName(string name);
        public bool Add(Role role);
        public void AddRange(params List<Role> roles);
        public bool Remove(Role role);
        public bool Update(Role role);
        public void Clear();
    }
}
