using LovatoOpticalApp.Application.DTOs;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ApiServiceResponse> CreateCustomer(CustomerResquestDto customerRequestDto);
        Task<ApiServiceResponse> UpdateCustomer(CustomerResquestDto customerRequestDto);
        Task<ApiServiceResponse> DeleteCustomer(Guid customerId);

    }
}
