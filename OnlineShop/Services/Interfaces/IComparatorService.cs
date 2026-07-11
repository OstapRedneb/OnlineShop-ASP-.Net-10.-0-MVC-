using OnlineShop.Models;

namespace OnlineShop.Services.Interfaces
{
    public interface IComparatorService
    {
        public List<Comparator> GetAll();
        public Comparator? GetById(Guid id);
        public bool Add(Comparator comparator);
        public void AddRange(params List<Comparator> comparators);
        public bool Update(Comparator comparator);
        public void Clear();
    }
}
