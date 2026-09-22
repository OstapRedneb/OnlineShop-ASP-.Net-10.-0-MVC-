using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string? Description { get; init; }
        public bool IsDeleted { get; set; } = false;
    }
}
