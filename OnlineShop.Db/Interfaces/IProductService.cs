using System;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces;

public interface IProductService
{
    public List<Product> GetAll();
        public List<Product> GetAllWithDeleted();
        public Product? GetById(Guid id);
        public bool Add(Product product);
        public void AddRange(params List<Product> products);
        public bool Update(Product product);
        public void Clear();
}
