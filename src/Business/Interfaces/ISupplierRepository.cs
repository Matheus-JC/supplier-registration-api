using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Business.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier?> GetSupplierWithAddress(Guid id);
    Task<Supplier?> GetSupplierWithProductsAndAddress(Guid id);

    Task<Address?> GetAddressBySupplier(Guid supplierId);
    Task DeleteSupplierAddress(Address address);
}