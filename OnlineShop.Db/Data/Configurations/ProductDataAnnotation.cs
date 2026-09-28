using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Data.DataAnnotations;

public class ProductDataAnnotation : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("product");

        entityTypeBuilder
            .Property("Id")
            .HasColumnName("id")
            .IsRequired(true);

        entityTypeBuilder
            .Property("Name")
            .HasColumnName("name")
            .IsRequired(true);

        entityTypeBuilder
            .Property("Price")
            .HasColumnName("price")
            .IsRequired(true);

        entityTypeBuilder
            .Property("Description")
            .HasColumnName("description")
            .IsRequired(true);

        entityTypeBuilder
            .Property("IsDeleted")
            .HasColumnName("is_deleted")
            .IsRequired(true);

        entityTypeBuilder.OnDelete()

        entityTypeBuilder.HasKey(product => product.Id);
    }
}
