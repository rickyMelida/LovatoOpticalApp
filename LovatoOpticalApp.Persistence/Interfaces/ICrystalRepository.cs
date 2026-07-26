using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface ICrystalRepository
    {
        Task<Crystal?> GetByIdAsync(Guid id);
        Task<IEnumerable<Crystal>> GetAllAsync();
    }
}
