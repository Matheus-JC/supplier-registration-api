using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Business.Interfaces;

public interface ISupplierService : IDisposable
{
    Task Create(Supplier supplier);
    Task Update(Supplier supplier);
    Task Delete(Guid id);
}