using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Coupons.DTOs
{
    public class CreateCouponDto
    {
        [Required]
        [StringLength(100)]
        public string CouponCode { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal MinAmount { get; set; }

        [StringLength(100)]
        public string? StripeId { get; set; }
    }
}
