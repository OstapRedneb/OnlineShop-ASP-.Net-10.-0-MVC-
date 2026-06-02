using System;

namespace OnlineShop.Models;

public record Product(Guid Id, string Name, decimal Cost)
{
    public Product() : this("TEST", decimal.MaxValue)
    {}
    public Product(string name, decimal cost) : this(Guid.NewGuid(), name, cost)
    { }
}
