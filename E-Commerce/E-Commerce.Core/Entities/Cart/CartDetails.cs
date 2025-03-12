using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.Core.Entities.Cart
{
    public class CartDetails : BaseEntity
    {
        // Foreign key linking to the cart header
        // Each cart detail must belong to a cart header
        [Required]
        [ForeignKey("CartHeader")]
        public Guid CartHeaderId { get; set; }

        // Foreign key linking to the product
        // Identifies which product is in the cart
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        // Quantity of the product in the cart
        // Must be between 1 and 100 items
        [Required]
        [Range(1, 100, ErrorMessage = "The value must be between 1 and 100.")]
        public int Count { get; set; }

        // Navigation properties for related entities
        // Enables easy access to the cart header and product information
        public CartHeader CartHeader { get; set; }
        public Product Product { get; set; }
    }
}
