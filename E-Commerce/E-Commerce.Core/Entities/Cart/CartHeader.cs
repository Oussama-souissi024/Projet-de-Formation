using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Core.Entities.Cart
{
    public class CartHeader : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        // Optional coupon code applied to this cart
        // Can be null if no coupon is applied
        public string? CouponCode { get; set; }

        // Navigation properties for related entities
        // Links to the user who owns the cart
        public ApplicationUser User { get; set; }

        // Collection of items in the cart
        // One cart header can have multiple cart details (products)
        public ICollection<CartDetails> CartDetails { get; set; }
    }
}
