using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customerDto);
        Task<Customer> UpdateCustomer(Customer customerDto);
        Task<Customer> GetCustomerDetails(Guid customerId);
        Task<Customer> GetCustomerByDoc(string doc);
        Task<List<Customer>> GetCustomers();
    }
}
