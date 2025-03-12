using E_Commerce.Application.CategoryE.DTOs;
using E_Commerce.Core.Entities.CategoryE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.CategoryE.Interfaces
{
    public interface ICategoryServise
    {
        Task<CategoryDto> CreateCategoryAsync(Category category);
        Task<CategoryDto> ReadByIdAsync(Guid categoryId);
        Task<IEnumerable<CategoryDto>> ReadAllAsync();
        Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName);
        Task Update(Category category);
        Task DeleteAsync(Guid categoryId);
    }
}
