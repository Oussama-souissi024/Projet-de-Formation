using AutoMapper;
using E_Commerce.Application.Cart.DTOs;
using E_Commerce.Core.Entities.Cart;

namespace E_Commerce.Application.Cart.Mappings
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // Configure CartHeader mappings
            // Enables two-way mapping between entity and DTO
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();

            // Configure CartDetails mappings
            // Enables two-way mapping between entity and DTO
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();

            // Configure Cart mappings
            // Enables two-way mapping between entity and DTO
            CreateMap<E_Commerce.Core.Entities.Cart.Cart, CartDto>().ReverseMap();
        }
    }
}
