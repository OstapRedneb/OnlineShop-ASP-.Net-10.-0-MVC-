using System;

namespace OnlineShop.Models;

public record Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required string Phone { get; init; }
    public DateTime Date { get; set; }
    public string? Coment { get; set; }

    public Order()
    { }

}
