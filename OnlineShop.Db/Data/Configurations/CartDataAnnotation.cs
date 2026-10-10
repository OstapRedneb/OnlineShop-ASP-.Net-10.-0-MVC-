using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Db.Data.Configurations
{
    public class CartDataAnnotation : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> entityTypeBuilder) 
        {
            entityTypeBuilder.ToTable("cart");
            entityTypeBuilder.HasKey(cart => cart.Id);
            entityTypeBuilder
                .HasMany(cart => cart.Positions)
                .WithOne(pos => pos.Cart)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.Property(cart => cart.Id).IsRequired(true).HasColumnName("id");
        }
    }
}
