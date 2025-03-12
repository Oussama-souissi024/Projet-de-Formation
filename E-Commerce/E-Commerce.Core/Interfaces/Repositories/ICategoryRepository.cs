using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Core.Entities.CategoryE;
using E_Commerce.Core.Interfaces.Repositories.Base;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> AddAsync(Category category);
        Task<Category> ReadByIdAsync(Guid categoryId);
        Task<IEnumerable<Category>> ReadAllAsync();
        Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName);
        Task Update(Category category);
        Task DeleteAsync(Guid categoryId);
    }
}
