using E_Commerce.Application.Products.DTOs;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> AddAsync(Product product);
        Task<ProductDto> ReadByIdAsync(Guid productId);
        Task<IEnumerable<ProductDto>> ReadAllAsync();
        void Update(Product product);
        Task DeleteAsync(Guid productId);
    }
}
