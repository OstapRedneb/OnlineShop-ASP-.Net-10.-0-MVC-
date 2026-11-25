using System;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services;

public class ProductServiceDb : IProductService
{
    private DatabaseContext _database = new DatabaseContext();
    public List<Product> GetAll()
    {
        return _database
            .Products
            .AsNoTracking()
            .Include(p => p.Positions)
            .Where(product => !product.IsDeleted)
            .ToList();
    }
    public List<Product> GetAllWithDeleted()
    {
        return _database
            .Products
            .AsNoTracking()
            .Include(p => p.Positions)
            .ToList();
    }
    public Product? GetById(Guid id)
    {
        Product product = _database.Products.AsNoTracking().FirstOrDefault(product => product.Id == id);
        _database.Entry(product).Collection(prod => prod.Positions).Load();
        return product;
    }
    public bool Add(Product? product)
    {
        if (product is null || _database.Products.Any(productInMemory => productInMemory.Id == product.Id))
            return false;

        _database.Products.Add(product);
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
