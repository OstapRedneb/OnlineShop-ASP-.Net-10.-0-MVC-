using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DatabaseContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connectionString = config.GetConnectionString("PostgreSqlConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
