using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplierRegServer.Api.DTOs;
using SupplierRegServer.Api.Extensions;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Api.Controllers.V1;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/suppliers")]
public class SupplierController : MainController
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly ISupplierService _supplierService;
    private readonly IMapper _mapper;

    public SupplierController(ISupplierRepository supplierRepository, ISupplierService supplierService,
        IMapper mapper, INotifier notifier) : base(notifier)
    {
        _supplierRepository = supplierRepository;
        _supplierService = supplierService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<SupplierDTO>> GetAll()
    {
        return _mapper.Map<IEnumerable<SupplierDTO>>(
            await _supplierRepository.GetAll()
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDTO>> GetById(Guid id)
    {
        var supplierDto = await GetSupplierById(id);

        if (supplierDto == null) return NotFound();

        return supplierDto;
    }

    [ClaimsAuthorize("Supplier", "Create")]
    [HttpPost]
    public async Task<ActionResult<SupplierDTO>> Create(SupplierDTO supplierDto)
    {
        if (!ModelState.IsValid) return HandleResponse(ModelState);

        await _supplierService.Create(_mapper.Map<Supplier>(supplierDto));

        return HandleResponse(HttpStatusCode.Created, supplierDto);
    }

    [ClaimsAuthorize("Supplier", "Update")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, SupplierDTO supplierDto)
    {
        if (id != supplierDto.Id)
        {
            NotifyError("The Ids informed entered are not the same");
            return HandleResponse();
        }

        if (!ModelState.IsValid) return HandleResponse(ModelState);

        await _supplierService.Update(_mapper.Map<Supplier>(supplierDto));

        return HandleResponse(HttpStatusCode.NoContent);
    }


    [ClaimsAuthorize("Supplier", "Delete")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<SupplierDTO>> Delete(Guid id)
    {
        var supplierDto = await GetSupplierById(id);

        if (supplierDto == null) return NotFound();

        await _supplierService.Delete(id);

        return HandleResponse(HttpStatusCode.NoContent);
    }

    private async Task<SupplierDTO> GetSupplierById(Guid id)
    {
        return _mapper.Map<SupplierDTO>(
            await _supplierRepository.GetSupplierWithProductsAndAddress(id)
        );
    }
}
