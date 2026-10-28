using System;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services;

public class ProductServiceDb : IProductService
{
    private DatabaseContext _database = new DatabaseContext();
    public List<Product> GetAll()
    {
        return _database.Products.Where(product => !product.IsDeleted).ToList();
    }
    public List<Product> GetAllWithDeleted()
    {
        return _database.Products.ToList();
    }
    public Product? GetById(Guid id)
    {
        return _database.Products.FirstOrDefault(product => product.Id == id);
    }
    public bool Add(Product? product)
    {
        if (product is null || _database.Products.Any(productInMemory => productInMemory.Id == product.Id))
            return false;

        _database.Add(product);
        _database.SaveChanges();

        return true;
    }
    public void AddRange(params List<Product> products)
    {
        products.ForEach(product => Add(product));
    }
    public bool Update(Product product)
    {
        _database.SaveChanges();
        return true;
    }
    public void Clear()
    {
        _database.Products.AddRange(_database.Products);
        _database.SaveChanges();
    }
}
