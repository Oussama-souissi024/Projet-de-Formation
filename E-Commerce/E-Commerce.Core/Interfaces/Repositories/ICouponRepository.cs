using E_Commerce.Core.Entities.Coupon;
using E_Commerce.Core.Interfaces.Repositories.Base;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Task<Coupon> AddAsync(Coupon coupon);
        Task<Coupon> ReadByIdAsync(Guid couponId);
        Task<Coupon> ReadByCouponCodeAsync(string couponCode);
        Task<IEnumerable<Coupon>> ReadAllAsync();
        Task UpdateAsync(Coupon coupon);
        Task DeleteAsync(Guid id);
    }
}
