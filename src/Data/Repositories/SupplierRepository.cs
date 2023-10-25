using Microsoft.EntityFrameworkCore;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Data.Context;

namespace Data.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    // Supplier
    public readonly ApplicationDbContext _appContext;
    public SupplierRepository(ApplicationDbContext context) : base(context)
    {
        _appContext = context;
    }

    public async Task<Supplier?> GetSupplierWithAddress(Guid id)
    {
        return await _appContext.Suppliers.AsNoTracking()
            .Include(s => s.Address)
            .FirstOrDefaultAsync(f => f.Id == id) ?? null;

    }
    public async Task<Supplier?> GetSupplierWithProductsAndAddress(Guid id)
    {
        return await _appContext.Suppliers.AsNoTracking()
            .Include(s => s.Address)
            .Include(s => s.Products)
            .FirstOrDefaultAsync(f => f.Id == id) ?? null;
    }

    // Address
    public async Task<Address?> GetAddressBySupplier(Guid supplierId)
    {
        return await _appContext.Address.AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<Guid>(a, "SupplierId") == supplierId);
    }

    public async Task DeleteSupplierAddress(Address address)
    {
        _appContext.Address.Remove(address);
        await SaveChanges();
    }
}
