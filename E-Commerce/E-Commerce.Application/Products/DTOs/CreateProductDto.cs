using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.Products.DTOs
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
