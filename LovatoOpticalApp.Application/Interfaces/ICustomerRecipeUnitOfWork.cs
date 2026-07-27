using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICustomerRecipeUnitOfWork
    {
        Task<ApiServiceResponse> CreateCustomerRecipeAsync(CustomerRecipeDtoRequest customerRecipeDtoRequest);
        Task<ApiServiceResponse> UpdateCustomerRecipeAsync(CustomerRecipeDtoRequest customerRecipeDtoRequest);
    }
}
