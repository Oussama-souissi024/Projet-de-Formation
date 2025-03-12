using System.ComponentModel.DataAnnotations;
using E_Commerce.Core.Common;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.Core.Entities.CategoryE
{
    public class Category : BaseEntity
    {

        [Required(ErrorMessage = "The category name is required.")]
        [MaxLength(100, ErrorMessage = "The category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string? Description { get; set; }


        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
