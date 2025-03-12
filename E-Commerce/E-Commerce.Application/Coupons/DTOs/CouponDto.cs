namespace E_Commerce.Application.Coupons.DTOs
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinAmount { get; set; }
        public string? StripeId { get; set; }
    }
}
