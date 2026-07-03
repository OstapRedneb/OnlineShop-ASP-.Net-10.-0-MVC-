using System;

namespace OnlineShop.Models;

public record Product(Guid Id, string Name, decimal Price, string? Description)
{
    public bool IsDeleted { get; set; } = false;
    public Product() : this("TEST", decimal.MaxValue)
    { }
    public Product(string name, decimal cost) : this(name, cost, null)
    { }
    public Product(string name, decimal cost, string? description) : this(Guid.NewGuid(), name, cost, description)
    { }
}
