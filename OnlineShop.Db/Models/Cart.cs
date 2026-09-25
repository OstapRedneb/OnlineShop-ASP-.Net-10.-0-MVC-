using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId;
        public List<Position> Positions { get; set; }
    }
}
