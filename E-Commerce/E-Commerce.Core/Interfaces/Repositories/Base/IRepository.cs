namespace E_Commerce.Core.Interfaces.Repositories.Base
{

    public interface IRepository<TEntity> where TEntity : class
    {
        
        Task<TEntity> AddAsync(TEntity entity);
        
        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task Update(TEntity entity);

        Task Remove(TEntity entity);

        Task<int> SaveChangesAsync();
    }
}
