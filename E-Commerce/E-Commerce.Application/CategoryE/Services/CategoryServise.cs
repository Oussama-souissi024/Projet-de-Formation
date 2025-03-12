using AutoMapper;
using E_Commerce.Application.CategoryE.DTOs;
using E_Commerce.Application.CategoryE.Interfaces;
using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Core.Entities.CategoryE;
using E_Commerce.Core.Interfaces.Repositories;

namespace E_Commerce.Application.CategoryE.Services
{
    public class CategoryServise : ICategoryServise
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServise(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(Category category)
        {
            var addedCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDto>(addedCategory);
        }
        public async Task<IEnumerable<CategoryDto>> ReadAllAsync()
        {
            var categories = await _categoryRepository.ReadAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        public async Task<CategoryDto> ReadByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.ReadByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category", categoryId);
            }
            return _mapper.Map<CategoryDto>(category);
        }
        public async Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName)
        {
            return await _categoryRepository.GetCategoryIdByCategoryNameAsync(categoryName);
        }
        public async Task Update(Category category)
        {
            var existingCategory = await _categoryRepository.ReadByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new NotFoundException("Category", category.Id);
            }
            await _categoryRepository.Update(category);
        }
        public async Task DeleteAsync(Guid categoryId)
        {
            var category = await _categoryRepository.ReadByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("Category", categoryId);
            }
            await _categoryRepository.DeleteAsync(categoryId);
        }
    }
}
