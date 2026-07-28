using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerResponseDto>> GetCustomers(PaginationParams parameters);
        Task<CustomerResponseDto> GetCustomerById(Guid customerId);
        Task<CustomerResponseDto> GetCustomerByDoc(string doc);
        Task<PagedResult<CustomerResponseDto>> SearchCustomer(string query, PaginationParams parameters);
        Task<CustomerResponseDto> CreateCustomer(CustomerResquestDto customerRequestDto);
        Task<CustomerResponseDto> UpdateCustomer(CustomerResquestDto customerRequestDto);
        Task DeleteCustomer(Guid customerId);

    }
}
