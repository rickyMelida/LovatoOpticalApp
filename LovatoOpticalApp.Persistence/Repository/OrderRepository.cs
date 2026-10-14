using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<Order?> GetByIdAsync(Guid id) =>
            throw new NotImplementedException("GetByIdAsync method is not implemented yet.");

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            throw new NotImplementedException("GetAllAsync method is not implemented yet.");

        public async Task AddAsync(Order order)
        {
            throw new NotImplementedException("AddAsync method is not implemented yet.");
        }
    }
}
