using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
    }
}
