using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Core.Interfaces
{
    public interface IProductService<TProduct> where TProduct : Product
    {
        Task<IEnumerable<TProduct>> GetAllAsync();
        Task<TProduct?> GetByIdAsync(int id);
        Task AddAsync(TProduct product);
        Task UpdateAsync(TProduct product);
        Task DeleteAsync(int id);
    }
}