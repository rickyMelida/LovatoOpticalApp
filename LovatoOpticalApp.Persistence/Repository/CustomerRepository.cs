using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
