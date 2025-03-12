using AutoMapper;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Coupons.DTOs;
using E_Commerce.Application.Coupons.Interfaces;
using E_Commerce.Core.Entities.Coupon;
using E_Commerce.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Application.Coupons.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponService(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public async Task<CouponDto> AddAsync(Coupon coupon)
        {
            var addedCoupon = await _couponRepository.AddAsync(coupon);
            return _mapper.Map<CouponDto>(addedCoupon);
        }

        public async Task DeleteAsync(Guid id)
        {
            var coupon = await _couponRepository.ReadByIdAsync(new Guid(id.ToString()));
            if (coupon == null)
            {
                throw new NotFoundException("Coupon", id);
            }
            await _couponRepository.DeleteAsync(new Guid(id.ToString()));
        }

        public async Task<CouponDto> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _couponRepository.ReadByCouponCodeAsync(couponCode);
            if (coupon == null)
            {
                throw new NotFoundException("Coupon", couponCode);
            }
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<IEnumerable<CouponDto>> ReadAllAsync()
        {
            var coupons = await _couponRepository.ReadAllAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(coupons);
        }

        public async Task<CouponDto> ReadByIdAsync(int couponId)
        {
            var coupon = await _couponRepository.ReadByIdAsync(new Guid(couponId.ToString()));
            if (coupon == null)
            {
                throw new NotFoundException("Coupon", couponId);
            }
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            var existingCoupon = await _couponRepository.ReadByIdAsync(coupon.Id);
            if (existingCoupon == null)
            {
                throw new NotFoundException("Coupon", coupon.Id);
            }
            await _couponRepository.Update(coupon);
        }
    }
}
