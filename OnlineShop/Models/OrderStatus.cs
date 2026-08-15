using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    [Display(Name = "CRRATED")]
    Created,
    [Display(Name = "PROCESSING")]
    Processing,
    [Display(Name = "SHIPPED")]
    Shipped,
    [Display(Name = "DELIVERED")]
    Delivered,
    [Display(Name = "CANCELLED")]
    Cancelled
}