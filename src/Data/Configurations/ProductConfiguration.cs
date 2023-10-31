using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnType("varchar(1000)")
            .IsRequired();

        builder.Property(p => p.Value)
           .HasColumnType("decimal(10,2)")
           .IsRequired();

        builder.ToTable(nameof(Product));
    }
}