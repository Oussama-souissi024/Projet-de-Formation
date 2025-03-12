using E_Commerce.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Cart.DTOs
{
        // DTO representing individual items in a shopping cart
        // Contains detailed information about each product in the cart
        public class CartDetailsDto
        {
            // Unique identifier for this cart detail entry
            public Guid CartDetailsId { get; set; }

            // ID of the cart header this detail belongs to
            public Guid CartHeaderId { get; set; }

            // Navigation property to access cart header information
            // Contains user and summary information
            public CartHeaderDto? CartHeader { get; set; }

            // ID of the product in this cart detail
            public Guid ProductId { get; set; }

            // Navigation property to access product information
            // Contains product details like name, price, etc.
            public ProductDto? Product { get; set; }

            // Quantity of the product in the cart
            public int Count { get; set; }

            // Price of the product at the time it was added to cart
            // Stored separately in case product price changes
            public double Price { get; set; }
        }
    }
