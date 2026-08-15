using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;

public record Order
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public OrderStatus Status { get; set; }

    [Display(Name = "NAME", Prompt = "YOUR_NAME")]
    [Required(ErrorMessage = "Field \"NAME\" is empty")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be longer than {2} and shourter than {1}")]
    [DataType(DataType.Text)]
    public string Name { get; init; }

    [Display(Name = "ADDRESS", Prompt = "YOUR_ADDRESS")]
    [Required(ErrorMessage = "Field \"ADDRESS\" is empty")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Address should be longer than {2} and shourter than {1}")]
    [DataType(DataType.Text)]
    public string Address { get; init; }

    [Display(Name = "PHONE", Prompt = "YOUR_PHONE")]
    [Required(ErrorMessage = "Field \"PHONE\" is empty")]
    [DataType(DataType.PhoneNumber, ErrorMessage = "You can white only your phone")]
    public string Phone { get; init; }

    public DateTime CreatedAt { get; init; }

    [Display(Name = "DATE_OF_ORDER")]
    [Required(ErrorMessage = "Field \"DATE_OF_ORDER\" is empty")]
    [DataType(DataType.Date, ErrorMessage = "You can white only date")]
    [DateRange()]
    public DateTime DeliveryDate { get; set; }

    [Display(Name = "COMMENT", Prompt = "YOUR_COMMENT")]
    [DataType(DataType.Text)]
    [StringLength(255)]
    public string? Comment { get; set; }

    public List<Position> Positions { get; init; }
    public decimal Price => Positions.Sum(position => position.Price);
    public int Count => Positions.Count;
    public int TotalCouunt => Positions.Sum(position => position.Quantity);

    public Guid UserId { get; set; }

    public Order() : this(new List<Position>())
    { }
    public Order(List<Position> positions)
    {
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Created;
        Positions = positions;
    }
}
