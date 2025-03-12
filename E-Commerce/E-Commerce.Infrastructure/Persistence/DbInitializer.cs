using E_Commerce.Core.Entities.CategoryE;
using E_Commerce.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Categories.AnyAsync())
            {
                await SeedCategories(context);
            }

            if (!await context.Products.AnyAsync())
            {
                await SeedProducts(context);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(ApplicationDbContext context)
        {
            var electronicsCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Électronique",
                Description = "Appareils électroniques et accessoires"
            };

            await context.Categories.AddAsync(electronicsCategory);
            await context.SaveChangesAsync();
        }

        private static async Task SeedProducts(ApplicationDbContext context)
        {
            var electronicsCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Électronique");
            if (electronicsCategory == null) return;

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Écran PC",
                    Description = "Écran LED 24 pouces haute résolution",
                    Price = 199.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "ecran-pc.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Unité Centrale",
                    Description = "Unité centrale avec processeur Intel Core i7 et 16 Go de RAM",
                    Price = 899.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "unite-centrale.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Clavier",
                    Description = "Clavier mécanique rétroéclairé",
                    Price = 59.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "clavier.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Souris",
                    Description = "Souris sans fil ergonomique avec capteur haute précision",
                    Price = 29.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "souris.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tapis de souris",
                    Description = "Tapis de souris XL antidérapant",
                    Price = 14.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "tapis-souris.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Casque audio",
                    Description = "Casque audio sans fil avec réduction de bruit",
                    Price = 129.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "casque-audio.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Webcam",
                    Description = "Webcam Full HD avec micro intégré",
                    Price = 79.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "webcam.jpg"
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
