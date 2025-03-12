using AutoMapper;
using E_Commerce.Application.Coupons.DTOs;
using E_Commerce.Core.Entities.Coupon;

namespace E_Commerce.Application.Coupons.Mappings
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>();
            
            CreateMap<CreateCouponDto, Coupon>();
            
            CreateMap<UpdateCouponDto, Coupon>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
