using LovatoOpticalApp.Core;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class CrystalOrderWorkRepository : ICrystalOrderWorkRepository
    {
        private readonly AppDbContext _context;

        public CrystalOrderWorkRepository(AppDbContext context) => _context = context;

        public async Task<CrystalOrderWork?> GetByIdAsync(Guid id) =>
            await _context.CrystalOrderWorks
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<CrystalOrderWork>> GetAllAsync() =>
            await _context.CrystalOrderWorks
                .ToListAsync();

        public async Task<(IEnumerable<CrystalOrderWork> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.CrystalOrderWorks
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<int> GetNextIndexAsync()
        {
            var maxIndex = await _context.CrystalOrderWorks
                .AsNoTracking()
                .Select(c => (int?)c.Index)
                .MaxAsync() ?? 0;

            return maxIndex + 1;
        }

        public async Task AddAsync(CrystalOrderWork crystalOrderWork)
        {
            await _context.CrystalOrderWorks.AddAsync(crystalOrderWork);
            await _context.SaveChangesAsync();
        }
    }
}
