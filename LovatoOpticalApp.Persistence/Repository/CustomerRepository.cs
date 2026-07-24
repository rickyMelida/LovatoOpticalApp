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
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if(customer.Id != Guid.Empty)
                return customer;
            

            await _context.Customers.AddAsync(customer);

            return customer;
        }

        public async Task<Customer> GetCustomerDetails(Guid customerId)
        {
            // Obtener un cliente CON todas sus recetas cargadas
            var customer = await _context.Customers
                .Include(c => c.Recipes)  // ← aquí sí se aprovecha la colección
                .FirstOrDefaultAsync(c => c.Id == customerId);

            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomer(Customer customerDto)
        {
            _context.Customers.Update(customerDto);
            
            return customerDto;
        }
    }
}
