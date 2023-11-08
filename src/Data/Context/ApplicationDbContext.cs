using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Data.Identity;

namespace SupplierRegServer.Data.Context;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entitiesTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var property in entitiesTypes.SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        foreach (var relationship in entitiesTypes.SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser>().ToTable("User", "security").HasData(InitialData.GetUsers());
        modelBuilder.Entity<IdentityRole>().ToTable("Role", "security").HasData(InitialData.GetRoles());
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security").HasData(InitialData.GetUsersRoles());
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security").HasData(InitialData.GetUserTestClaims());
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleCreationDateProperty();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void HandleCreationDateProperty()
    {
        var propertyName = "CreationDate";
        var entries = ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(propertyName) != null);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(propertyName).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(propertyName).IsModified = false;
            }
        }
    }
}