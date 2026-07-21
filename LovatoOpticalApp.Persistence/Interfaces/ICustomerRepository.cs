using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customerDto);
        Task<List<Customer>> GetCustomers();
    }
}
