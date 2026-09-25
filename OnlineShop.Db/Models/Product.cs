using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineShop.Db.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        [Required]
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        public decimal Price { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; }

        [Column("positions")]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public List<Position> Positions { get; set; }
    }
}
