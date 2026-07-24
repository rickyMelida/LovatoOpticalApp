using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Persistence;

namespace LovatoOpticalApp.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
