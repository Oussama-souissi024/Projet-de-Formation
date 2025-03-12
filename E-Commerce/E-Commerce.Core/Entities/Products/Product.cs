using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.CategoryE;

namespace E_Commerce.Core.Entities.Products
{
    public class Product : BaseEntity
    {
        // The name of the product, limited to 200 characters
        // Required field - cannot be null or empty
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        // The price of the product with 2 decimal places
        // Uses decimal for precise monetary calculations
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        // Detailed description of the product
        // Optional field - can be null
        public string Description { get; set; }

        // Foreign key linking to the Category table
        // Each product must belong to a category
        [Required]
        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }

        // URL to the product's image
        // Optional field - can be null
        public string ImageUrl { get; set; }

        // Navigation property to access the associated Category
        // Enables easy access to category information through Entity Framework
        public Category Category { get; set; }

        // Navigation properties for related cart and order details
        // One product can be in multiple shopping carts and orders
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
