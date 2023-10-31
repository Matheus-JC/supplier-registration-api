using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(s => s.Document)
            .HasColumnType("varchar(14)")
            .IsRequired();

        builder.HasOne(s => s.Address)
            .WithOne(a => a.Supplier);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier);

        builder.ToTable(nameof(Supplier));
    }
}