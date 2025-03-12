using E_Commerce.Core.Entities.Coupon;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        private readonly ApplicationDbContext _context;

        public CouponRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Coupon> AddAsync(Coupon coupon)
        {
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }

        public async Task<IEnumerable<Coupon>> ReadAllAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> ReadByCouponCodeAsync(string couponCode)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode.ToLower() == couponCode.ToLower());
        }

        public async Task<Coupon> ReadByIdAsync(Guid couponId)
        {
            return await _context.Coupons
                .FirstOrDefaultAsync(c => c.Id == couponId);
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            _context.Entry(coupon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();
            }
        }
    }
}
