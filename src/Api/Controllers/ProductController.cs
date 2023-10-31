using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupplierRegServer.Api.DTOs;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Api.Controllers;

[Route("api/products")]
public class ProductController : MainController
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, IProductService productService,
        IMapper mapper, INotifier notifier) : base(notifier)
    {
        _productRepository = productRepository;
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<ProductDTO>>(
            await _productRepository.GetProductsWithSuppliers()
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDTO>> GetById(Guid id)
    {
        var productDto = await GetProductById(id);

        if (productDto == null) return NotFound();

        return productDto;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDto)
    {
        if (!ModelState.IsValid) return HandleResponse(ModelState);

        await _productService.Create(_mapper.Map<Product>(productDto));

        return HandleResponse(HttpStatusCode.Created, productDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ProductDTO productDto)
    {
        if (id != productDto.Id)
        {
            NotifyError("The Ids informed entered are not the same");
            return HandleResponse();
        }

        if (!ModelState.IsValid) return HandleResponse(ModelState);

        var productUpdate = await GetProductById(id);

        productUpdate.SupplierId = productDto.SupplierId;
        productUpdate.Name = productDto.Name;
        productUpdate.Description = productDto.Description;
        productUpdate.Value = productDto.Value;
        productUpdate.Active = productDto.Active;

        await _productService.Update(_mapper.Map<Product>(productUpdate));

        return HandleResponse(HttpStatusCode.NoContent);
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductDTO>> Delete(Guid id)
    {
        var productDto = await GetProductById(id);

        if (productDto == null) return NotFound();

        await _productService.Delete(id);

        return HandleResponse(HttpStatusCode.NoContent);
    }

    private async Task<ProductDTO> GetProductById(Guid id)
    {
        return _mapper.Map<ProductDTO>(
            await _productRepository.GetProducWithSupplier(id)
        );
    }
}
