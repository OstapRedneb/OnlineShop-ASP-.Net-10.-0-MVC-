using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products;
    }
}
