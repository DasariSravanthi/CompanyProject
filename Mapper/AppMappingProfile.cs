using AutoMapper;

using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<ProductDto, Product>();

        CreateMap<ProductDetailDto, ProductDetail>();
        
    }
}