using E_Commerce.Application.Coupons.DTOs;
using E_Commerce.Application.Orders.DTOs;
using E_Commerce.Application.Payments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Payments.Interfaces
{
    public interface IPaymentService
    {
        Task<StripeRequestDto?> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto);
        Task<OrderHeaderDto> ValidateStripeSession(int orderHeaderId);
        Task<string> AddStripeCoupon(CouponDto couponDto);
        Task<string> DeleteStripeCoupon(CouponDto couponDto);
        Task<bool> StripeRefundOptions(string paymentIntentId);
    }
}
