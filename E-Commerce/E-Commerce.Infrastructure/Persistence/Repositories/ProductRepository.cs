using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Repositories.Base;
using E_Commerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
             await _context.AddAsync(product);
            _context.SaveChangesAsync();
            return product;
        }

        public  async Task<Product> ReadByIdAsync(Guid productId)
        {
            return await _context.Products
                                 .Include(p => p.Category) // Include navigation to Category
                                 .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> ReadAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            Update(product);
            await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var product = await GetByIdAsync(productId);
            if (product == null) return false;

            Remove(product);
            return await SaveChangesAsync() > 0;
        }
    }
}
