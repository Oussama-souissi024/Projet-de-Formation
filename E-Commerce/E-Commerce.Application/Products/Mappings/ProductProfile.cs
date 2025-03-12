using AutoMapper;
using E_Commerce.Application.Products.DTOs;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.Application.Products.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductDto, Product>();

        }
    }
}
