using E_Commerce.Core.Entities.Cart;
using E_Commerce.Core.Entities.Orders;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Entities.Coupon;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Core.Entities.CategoryE;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_Commerce.Core.Entities.Identity;

namespace E_Commerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Entity DbSet Definitions
        // DbSet properties represent database tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        #endregion

        #region Entity Configuration
        // Configure entity relationships using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base configuration for Identity tables
            base.OnModelCreating(modelBuilder);

            #region Shopping Cart Relationships
            // Configure Cart Header - User relationship (1:Many)
            modelBuilder.Entity<CartHeader>()
                .HasOne(c => c.User)                    // CartHeader has one User
                .WithMany(u => u.CartHeaders)           // User has many CartHeaders
                .HasForeignKey(c => c.UserId)          // Foreign key property
                .OnDelete(DeleteBehavior.Cascade);     // Delete cart when user is deleted

            // Configure Cart Details - Cart Header relationship (1:Many)
            modelBuilder.Entity<CartDetails>()
                .HasOne(c => c.CartHeader)
                .WithMany(ch => ch.CartDetails)
                .HasForeignKey(c => c.CartHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Cart Details - Product relationship (1:Many)
            modelBuilder.Entity<CartDetails>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartDetails)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);    // Prevent product deletion if in cart
            #endregion

            #region Order Relationships
            // Configure Order Header - User relationship (1:Many)
            modelBuilder.Entity<OrderHeader>()
                .HasOne(o => o.User)
                .WithMany(u => u.OrderHeaders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order Details - Order Header relationship (1:Many)
            modelBuilder.Entity<OrderDetails>()
                .HasOne(o => o.OrderHeader)
                .WithMany(oh => oh.OrderDetails)
                .HasForeignKey(o => o.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order Details - Product relationship (1:Many)
            modelBuilder.Entity<OrderDetails>()
                .HasOne(o => o.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);    // Prevent product deletion if in order
            #endregion
        }
        #endregion
    }
}

