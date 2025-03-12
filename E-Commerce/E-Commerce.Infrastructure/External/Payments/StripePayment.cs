using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Interfaces.External;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;

namespace E_Commerce.Infrastructure.External.Payments
{
    public class StripePayment : IStripePayment
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripePayment> _logger;

        public StripePayment(IConfiguration configuration, ILogger<StripePayment> logger)
        {
            _configuration = configuration;
            _logger = logger;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<(string SessionId, string SessionUrl)?> CreateCheckoutSession(OrderHeader orderHeader, string approvedUrl, string cancelUrl)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = approvedUrl,
                    CancelUrl = cancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                // Add discount if applicable
                if (!string.IsNullOrEmpty(orderHeader.CouponCode))
                {
                    options.Discounts = new List<SessionDiscountOptions>
                    {
                        new SessionDiscountOptions
                        {
                            Coupon = orderHeader.CouponCode
                        }
                    };
                }

                // Add line items
                foreach (var item in orderHeader.OrderDetails)
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

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return (session.Id, session.Url);
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error creating checkout session");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating checkout session");
                return null;
            }
        }

        public async Task<(string Status, string? PaymentIntentId)> ValidatePaymentSession(string sessionId)
        {
            try
            {
                var sessionService = new SessionService();
                var session = await sessionService.GetAsync(sessionId);

                if (session == null)
                {
                    return ("Error", null);
                }

                if (session.PaymentStatus == "unpaid")
                {
                    return ("Pending", null);
                }

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId);

                return paymentIntent.Status.ToLower() switch
                {
                    "succeeded" => ("Approved", paymentIntent.Id),
                    "requires_payment_method" => ("PaymentRequired", null),
                    "requires_action" => ("PaymentPending", null),
                    _ => ("PaymentFailed", null)
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error validating session {SessionId}", sessionId);
                return ($"Stripe Error: {ex.StripeError.Message}", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating session {SessionId}", sessionId);
                return ($"Error: {ex.Message}", null);
            }
        }

        public async Task<string> CreateCoupon(string couponCode, decimal discountAmount)
        {
            try
            {
                var options = new CouponCreateOptions
                {
                    Duration = "once",
                    AmountOff = (long)(discountAmount * 100),
                    Name = couponCode,
                    Currency = "usd",
                    Id = couponCode
                };

                var service = new CouponService();
                var coupon = await service.CreateAsync(options);

                return coupon != null && coupon.Valid ? coupon.Id : "Failed to create coupon";
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error creating coupon {CouponCode}", couponCode);
                return $"Stripe Error: {ex.StripeError.Message}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating coupon {CouponCode}", couponCode);
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> DeleteCoupon(string couponCode)
        {
            try
            {
                var service = new CouponService();
                await service.DeleteAsync(couponCode);
                return string.Empty;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error deleting coupon {CouponCode}", couponCode);
                return $"Stripe Error: {ex.StripeError.Message}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting coupon {CouponCode}", couponCode);
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> ProcessRefund(string paymentIntentId)
        {
            try
            {
                var options = new RefundCreateOptions
                {
                    PaymentIntent = paymentIntentId,
                    Reason = RefundReasons.RequestedByCustomer
                };

                var service = new RefundService();
                var refund = await service.CreateAsync(options);

                return refund.Status == "succeeded";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing refund for payment {PaymentIntentId}", paymentIntentId);
                return false;
            }
        }
    }
}
