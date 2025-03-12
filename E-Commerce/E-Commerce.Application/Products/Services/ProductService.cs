using AutoMapper;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Products.DTOs;
using E_Commerce.Application.Products.Interfaces;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Interfaces.Repositories;

namespace E_Commerce.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddAsync(Product product)
        {
            var addedProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(addedProduct);
        }

        public async Task DeleteAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product", productId);
            }
            await _productRepository.DeleteAsync(productId);
        }

        public async Task<IEnumerable<ProductDto>> ReadAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> ReadByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product", productId);
            }
            return _mapper.Map<ProductDto>(product);
        }

        public void Update(Product product)
        {
            var existingProduct = _productRepository.GetByIdAsync(product.Id).Result;
            if (existingProduct == null)
            {
                throw new NotFoundException("Product", product.Id);
            }
            _productRepository.UpdateAsync(product).Wait();
        }
    }
}
