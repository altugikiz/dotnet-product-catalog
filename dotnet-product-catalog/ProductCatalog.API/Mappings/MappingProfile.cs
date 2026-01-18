using AutoMapper;
using ProductCatalog.API.Dtos;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // // Product -> ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "None"));

        // CreateProductDto -> Product
        CreateMap<CreateProductDto, Product>();
    }
}