using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class CrystalRepository : ICrystalRepository
    {
        private readonly AppDbContext _context;

        public CrystalRepository(AppDbContext context) => _context = context;

        public async Task<Crystal?> GetByIdAsync(Guid id) =>
            await _context.Crystals
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Crystal>> GetAllAsync() =>
            await _context.Crystals
                .ToListAsync();
    }
}
