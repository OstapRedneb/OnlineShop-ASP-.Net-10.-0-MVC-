using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
