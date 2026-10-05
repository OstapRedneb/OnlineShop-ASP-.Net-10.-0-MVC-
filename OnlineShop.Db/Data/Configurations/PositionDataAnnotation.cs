using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Data.Configurations;

public class PositionDataAnnotation : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("positions");
        entityTypeBuilder
            .Property(position => position.Id)
            .HasColumnName("id")
            .IsRequired(true);

        entityTypeBuilder.HasOne(pos => pos.Product).WithMany(product => product.Positions);
        entityTypeBuilder.HasOne(pos => pos.Cart).WithMany(cart => cart.Positions);
        entityTypeBuilder.HasKey(pos => pos.Id);
    }
}
