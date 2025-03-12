using E_Commerce.Application.Coupons.DTOs;
using E_Commerce.Core.Entities.Coupon;

namespace E_Commerce.Application.Coupons.Interfaces
{
    public interface ICouponService
    {
        Task<CouponDto> AddAsync(Coupon coupon);
        Task<CouponDto> ReadByIdAsync(int couponId);
        Task<CouponDto> GetCouponByCodeAsync(string couponCode);
        Task<IEnumerable<CouponDto>> ReadAllAsync();
        Task UpdateAsync(Coupon coupon);
        Task DeleteAsync(Guid id);
    }
}
