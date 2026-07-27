using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface IGlassesCaseRepository
    {
        Task<GlassesCase?> GetByIdAsync(Guid id);
        Task<IEnumerable<GlassesCase>> GetAllAsync();
    }
}
