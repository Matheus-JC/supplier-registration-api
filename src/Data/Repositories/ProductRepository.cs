using Microsoft.EntityFrameworkCore;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Data.Context;

namespace SupplierRegServer.Data.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public readonly ApplicationDbContext _appContext;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _appContext = context;
    }

    public async Task<Product?> GetProducWithSupplier(Guid id)
    {
        return await _appContext.Products.AsNoTracking()
            .Include(s => s.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id) ?? null;
    }

    public async Task<IEnumerable<Product>> GetProductsWithSuppliers()
    {
        return await _appContext.Products.AsNoTracking()
            .Include(s => s.Supplier)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
    {
        return await Search(p => p.SupplierId == supplierId);
    }
}
