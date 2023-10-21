using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Business.Models.Validations;

namespace SupplierRegServer.Business.Services;

public class ProductService : BaseService, IProductService
{
    public readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
    {
        _productRepository = productRepository;
    }

    public async Task Create(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Create(product);
    }

    public async Task Update(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        await _productRepository.Update(product);
    }

    public async Task Delete(Guid id)
    {
        var product = await _productRepository.GetById(id);

        if (product == null)
        {
            Notify("Product doesn't exist");
            return;
        }

        await _productRepository.Delete(id);
    }

    public void Dispose()
    {
        _productRepository.Dispose();
    }
}
