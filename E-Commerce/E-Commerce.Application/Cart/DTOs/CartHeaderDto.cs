using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Cart.DTOs
{
    public class CartHeaderDto
    {
        // Unique identifier for the cart header
        public Guid CartHeaderId { get; set; }

        // ID of the user who owns this cart
        public string UserId { get; set; }

        // Coupon code applied to this cart for discounts
        public string? CouponCode { get; set; }

        // Amount discounted from the cart total
        public decimal Discount { get; set; }

        // Total amount of all items in the cart after discount
        public decimal CartTotal { get; set; }

        // Customer contact information
        // Required for order processing and communication
        [Required]
        public string? Name { get; set; }

        // Customer phone number for order updates
        [Required]
        public string? Phone { get; set; }

        // Customer email for order confirmation and updates
        [Required]
        public string? Email { get; set; }
    }
}
