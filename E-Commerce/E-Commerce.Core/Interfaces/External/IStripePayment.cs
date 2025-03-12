using E_Commerce.Core.Entities.Orders;

namespace E_Commerce.Core.Interfaces.External
{
    public interface IStripePayment
    {
        Task<(string SessionId, string SessionUrl)?> CreateCheckoutSession(OrderHeader orderHeader, string approvedUrl, string cancelUrl);
        Task<(string Status, string? PaymentIntentId)> ValidatePaymentSession(string sessionId);
        Task<string> CreateCoupon(string couponCode, decimal discountAmount);
        Task<string> DeleteCoupon(string couponCode);
        Task<bool> ProcessRefund(string paymentIntentId);
    }
}
