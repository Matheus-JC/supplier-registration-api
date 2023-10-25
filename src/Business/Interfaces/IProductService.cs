using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Business.Interfaces;

public interface IProductService : IDisposable
{
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}