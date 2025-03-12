using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.Orders;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Range(1000, 99999, ErrorMessage = "Zip Code must be between 1000 and 99999.")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string? Address { get; set; }

        // Navigation properties for related entities
        // Links to user's shopping carts
        // One user can have multiple shopping carts over time
        public ICollection<CartHeader> CartHeaders { get; set; }

        // Links to user's orders
        // One user can have multiple orders
        public ICollection<OrderHeader> OrderHeaders { get; set; }
    }
}
