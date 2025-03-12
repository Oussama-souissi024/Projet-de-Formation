using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Entities.Cart;

namespace E_Commerce.Core.Entities.Coupon
{
    public class Coupon : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string CouponCode { get; set; }

        // The amount of discount this coupon provides
        // Stored with 2 decimal places for precise calculations
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DiscountAmount { get; set; }

        // Minimum order amount required to use this coupon
        // Ensures coupons are used only on qualifying orders
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MinAmount { get; set; }

        // The ID of the corresponding coupon in Stripe
        // Optional field that links our coupon to Stripe's payment system
        [MaxLength(100)]
        public string? StripeId { get; set; }  // Store the Stripe coupon ID

        // Navigation properties to track where this coupon is used
        // One coupon can be used in multiple cart headers and order headers
        public ICollection<CartHeader> CartHeaders { get; set; }
        public ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}