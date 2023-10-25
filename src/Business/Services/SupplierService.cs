using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Business.Models.Validations;

namespace SupplierRegServer.Business.Services;

public class SupplierService : BaseService, ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository, INotifier notifier) : base(notifier)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task Create(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

        if (supplier.Address != null && !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

        var supplierExists = _supplierRepository.Search(f => f.Document == supplier.Document).Result.Any();
        if (supplierExists)
        {
            Notify("There is already a supplier with the informed document");
            return;
        }

        await _supplierRepository.Create(supplier);
    }

    public async Task Update(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

        var supplierExists = _supplierRepository.Search(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any();

        if (supplierExists)
        {
            Notify("There is already a supplier with the informed document");
            return;
        }

        await _supplierRepository.Update(supplier);
    }

    public async Task Delete(Guid id)
    {
        var supplier = await _supplierRepository.GetSupplierWithProductsAndAddress(id);

        if (supplier == null)
        {
            Notify("Supplier doesn't exist");
            return;
        }

        if (supplier.Products != null && supplier.Products.Any())
        {
            Notify("It is not possible to delete the supplier because it has registered products");
            return;
        }

        var address = await _supplierRepository.GetAddressBySupplier(id);

        if (address != null)
        {
            await _supplierRepository.DeleteSupplierAddress(address);
        }

        await _supplierRepository.Delete(id);
    }

    public void Dispose()
    {
        _supplierRepository.Dispose();
    }
}
