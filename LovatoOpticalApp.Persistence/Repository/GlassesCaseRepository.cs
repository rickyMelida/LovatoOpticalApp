using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class GlassesCaseRepository : IGlassesCaseRepository
    {
        private readonly AppDbContext _context;

        public GlassesCaseRepository(AppDbContext context) => _context = context;

        public async Task<GlassesCase?> GetByIdAsync(Guid id) =>
            await _context.GlassesCases.FindAsync(id);

        public async Task<IEnumerable<GlassesCase>> GetAllAsync() =>
            await _context.GlassesCases.ToListAsync();
    }
}
