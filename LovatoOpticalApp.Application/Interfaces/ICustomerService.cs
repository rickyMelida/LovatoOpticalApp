using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ApiServiceResponse> CreateCustomer(CustomerResquestDto customerRequestDto);
        Task<ApiServiceResponse> UpdateCustomer(CustomerResquestDto customerRequestDto);
        Task<ApiServiceResponse> DeleteCustomer(Guid customerId);

    }
}
