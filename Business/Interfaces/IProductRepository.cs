using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Business.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId);
    Task<IEnumerable<Product>> GetProductsWithSuppliers();
    Task<Product> GetProducWithSupplier(Guid id);
}