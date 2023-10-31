using AutoMapper;
using SupplierRegServer.Api.DTOs;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Api.Configurations;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Supplier, SupplierDTO>().ReverseMap();
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));
    }
}
