using System;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Services;

public class CartServiceDb : ICartService
{
    private DatabaseContext _database = new DatabaseContext();
    public List<Cart> GetAll()
    {
        return _database.Carts.ToList();
    }
    public Cart? GetById(Guid id)
    {
        return _database.Carts.FirstOrDefault(cart => cart.Id == id);
    }
    public bool Add(Cart? cart)
    {
        if (cart is null || _database.Carts.Any(cartInMemory => cartInMemory.Id == cart.Id))
            return false;

        _database.Add(cart);
        _database.SaveChanges();

        return true;
    }
    public void AddRange(params List<Cart> carts)
    {
        carts.ForEach(cart => Add(cart));
    }
    public bool Update(Cart cart)
    {
        _database.SaveChanges();
        return true;
    }
    public void Clear()
    {
        _database.Carts.AddRange(_database.Carts);
        _database.SaveChanges();
    }
}
