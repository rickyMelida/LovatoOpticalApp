using LovatoOpticalApp.Core;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface ICrystalOrderWorkRepository
    {
        Task<CrystalOrderWork?> GetByIdAsync(Guid id);
        Task<IEnumerable<CrystalOrderWork>> GetAllAsync();
        Task<(IEnumerable<CrystalOrderWork> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> GetNextIndexAsync();
        Task AddAsync(CrystalOrderWork crystalOrderWork);
    }
}
