using AutoMapper;
using E_Commerce.Application.Cart.DTOs;
using E_Commerce.Application.Orders.DTOs;
using E_Commerce.Core.Entities.Orders;

namespace E_Commerce.Application.Orders.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal))
                .ReverseMap();

            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

            CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();

            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            CreateMap<OrderDetailsDto, CartDetailsDto>().ReverseMap();
        }
    }
}
