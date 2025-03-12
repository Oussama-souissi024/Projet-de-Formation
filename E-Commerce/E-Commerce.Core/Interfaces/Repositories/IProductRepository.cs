using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Interfaces.Repositories.Base;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> AddAsync(Product product);
        Task<Product> ReadByIdAsync(Guid productId);
        Task<IEnumerable<Product>> ReadAllAsync();
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid productId);
    }
}
