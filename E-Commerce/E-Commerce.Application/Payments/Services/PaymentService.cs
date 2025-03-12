using E_Commerce.Application.Payments.DTOs;
using E_Commerce.Application.Payments.Interfaces;
using E_Commerce.Core.Interfaces.External;
using E_Commerce.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using E_Commerce.Application.Coupons.DTOs;
using Stripe;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Application.Orders.DTOs;

namespace E_Commerce.Application.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IStripePayment _stripePayment;
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<PaymentService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentService(
            IStripePayment stripePayment,
            IOrderRepository orderRepository,
            ICouponRepository couponRepository,
            ILogger<PaymentService> logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            _stripePayment = stripePayment;
            _orderRepository = orderRepository;
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<StripeRequestDto?> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" },
                };
                var DiscountsObj = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions
                    {
                        Coupon=stripeRequestDto.OrderHeader.CouponCode
                    }
                };
                foreach (var item in stripeRequestDto.OrderHeader.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ProductName,
                                Description = $"Quantity: {item.Count}"
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                if (stripeRequestDto.OrderHeader.Discount > 0)
                {
                    options.Discounts = DiscountsObj;
                }
                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                stripeRequestDto.StripeSessionUrl = session.Url;
                stripeRequestDto.StripeSessionId = session.Id;

                OrderHeader orderHeader = await _orderRepository.GetByIdAsync(stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeader.StripeSessionId = session.Id;
                _orderRepository.UpdateOrderHeaderAsync(orderHeader);

                return stripeRequestDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating stripe session");
                return null;
            }
        }

        public async Task<OrderHeaderDto> ValidateStripeSession(int orderHeaderId)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                OrderHeader orderHeader = await _orderRepository.GetByIdAsync(new Guid(orderHeaderId.ToString()));

                if (orderHeader == null)
                {
                    _logger.LogWarning("Order header not found: {OrderId}", orderHeaderId);
                    return new OrderHeaderDto { Status = "Order not found" };
                }

                var (status, paymentIntentId) = await _stripePayment.ValidatePaymentSession(orderHeader.StripeSessionId);

                orderHeader.Status = status;
                if (paymentIntentId != null)
                {
                    orderHeader.PaymentIntentId = paymentIntentId;
                }

                await _orderRepository.UpdateOrderHeaderAsync(orderHeader);
                return _mapper.Map<OrderHeaderDto>(orderHeader);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating stripe session for order {OrderId}", orderHeaderId);
                return new OrderHeaderDto { Status = $"Error: {ex.Message}" };
            }
        }

        public async Task<string> AddStripeCoupon(CouponDto couponDto)
        {
            try
            {
                var result = await _stripePayment.CreateCoupon(
                    couponDto.CouponCode,
                    couponDto.DiscountAmount);

                if (string.IsNullOrEmpty(result) || !result.StartsWith("Error"))
                {
                    var coupon = await _couponRepository.ReadByCouponCodeAsync(couponDto.CouponCode);
                    if (coupon != null)
                    {
                        coupon.StripeId = result;
                        await _couponRepository.UpdateAsync(coupon);
                    }
                    return string.Empty;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding stripe coupon {CouponCode}", couponDto.CouponCode);
                return ex.Message;
            }
        }

        public async Task<string> DeleteStripeCoupon(CouponDto couponDto)
        {
            try
            {
                return await _stripePayment.DeleteCoupon(couponDto.CouponCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting stripe coupon {CouponCode}", couponDto.CouponCode);
                return ex.Message;
            }
        }

        public async Task<bool> StripeRefundOptions(string paymentIntentId)
        {
            try
            {
                return await _stripePayment.ProcessRefund(paymentIntentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing refund for payment {PaymentId}", paymentIntentId);
                return false;
            }
        }
    }
}
