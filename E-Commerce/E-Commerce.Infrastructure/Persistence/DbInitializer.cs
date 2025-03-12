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
                Name = "�lectronique",
                Description = "Appareils �lectroniques et accessoires"
            };

            await context.Categories.AddAsync(electronicsCategory);
            await context.SaveChangesAsync();
        }

        private static async Task SeedProducts(ApplicationDbContext context)
        {
            var electronicsCategory = await context.Categories.FirstOrDefaultAsync(c => c.Name == "�lectronique");
            if (electronicsCategory == null) return;

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "�cran PC",
                    Description = "�cran LED 24 pouces haute r�solution",
                    Price = 199.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "ecran-pc.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Unit� Centrale",
                    Description = "Unit� centrale avec processeur Intel Core i7 et 16 Go de RAM",
                    Price = 899.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "unite-centrale.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Clavier",
                    Description = "Clavier m�canique r�tro�clair�",
                    Price = 59.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "clavier.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Souris",
                    Description = "Souris sans fil ergonomique avec capteur haute pr�cision",
                    Price = 29.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "souris.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tapis de souris",
                    Description = "Tapis de souris XL antid�rapant",
                    Price = 14.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "tapis-souris.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Casque audio",
                    Description = "Casque audio sans fil avec r�duction de bruit",
                    Price = 129.99m,
                    CategoryID = electronicsCategory.Id,
                    ImageUrl = "casque-audio.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Webcam",
                    Description = "Webcam Full HD avec micro int�gr�",
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
