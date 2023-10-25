using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierRegServer.Business.Models;

namespace Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.PublicArea)
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(s => s.Number)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(s => s.ZipCode)
            .HasColumnType("varchar(8)")
            .IsRequired();

        builder.Property(s => s.Neighborhood)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(s => s.City)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(s => s.State)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(s => s.Country)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(s => s.Complement)
            .HasColumnType("varchar(250)");

        builder.ToTable("Addresses");
    }
}
