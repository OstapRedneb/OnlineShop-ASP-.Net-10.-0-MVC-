using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public record Product
{
    public Guid Id { get; init; }

    [Display(Name = "NAME", Prompt = "PRODUCT_NAME")]
    [Required(ErrorMessage = "Field \"NAME\" is empty")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "string length should be more than {2} symbol and less then {1} symbols")]
    [DataType(DataType.Text)]
    public string Name { get; init; }

    [Display(Name = "PRICE")]
    [Required(ErrorMessage = "Field \"PRICE\" is empty")]
    [Range(1, 9_999_999, ErrorMessage = "Price should be more than {1} and less than {2}")]
    [DataType(DataType.Currency)]
    public decimal Price { get; init; }

    [Display(Name = "DESCRIPTION", Prompt = "PRODUCT_DESCRIPTION")]
    [StringLength(10_000)]
    [DataType(DataType.Text)]
    public string? Description { get; init; }

    public bool IsDeleted { get; set; } = false;


    public Product() : this("TEST", 0)
    { }
    public Product(string name, decimal cost) : this(name, cost, null)
    { }
    public Product(string name, decimal cost, string? description) : this(Guid.NewGuid(), name, cost, description)
    { }
    public Product(Guid id, string name, decimal price, string? description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}
