using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Core.Entities.Orders
{
    public class OrderHeader : BaseEntity
    {
        // ID of the user who placed the order
        // Links to ApplicationUser table
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Optional coupon code applied to this order
        public string? CouponCode { get; set; }

        // Amount discounted from the order total
        // Stored with 2 decimal places
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Discount { get; set; }

        // Final total amount of the order after discounts
        // Required field with 2 decimal places
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal OrderTotal { get; set; }

        // Customer contact information
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Timestamp when the order was placed
        [Required]
        public DateTime OrderTime { get; set; }

        // Current status of the order (e.g., Pending, Approved, Shipped)
        public string Status { get; set; }

        // Stripe payment processing fields
        // Stores IDs from Stripe for payment tracking
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        // Navigation properties for related entities
        // Links to the user who placed the order
        public ApplicationUser User { get; set; }

        // Collection of items in this order
        // One order can have multiple order details
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
