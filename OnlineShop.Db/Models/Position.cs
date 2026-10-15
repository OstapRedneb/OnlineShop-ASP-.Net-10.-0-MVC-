using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public decimal Quantity
        {
            get => field;
            set
            {
                if (field < 0)
                    field = 0;
                field = value;
            }
        }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
