using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence
{
    public interface IProductRepository<T> where T : Product
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> SearchAsync(string query);
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
