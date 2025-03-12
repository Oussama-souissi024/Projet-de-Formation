using E_Commerce.Application.Products.DTOs;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Orders.DTOs
{
    public class OrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
