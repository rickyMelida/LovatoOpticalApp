using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
    public class CustomerRecipeUnitOfWork : ICustomerRecipeUnitOfWork
    {
        private readonly ICustomerService _customerService;
        private readonly IRecipeService _recipeService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerRecipeUnitOfWork(ICustomerService customerService, IRecipeService recipeService, IUnitOfWork unitOfWork)
        {
            _customerService = customerService;
            _recipeService = recipeService;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiServiceResponse> CreateCustomerRecipeAsync(CustomerRecipeDtoRequest customerRecipeDtoRequest)
        {
            try
            {
                var customer = await _customerService.CreateCustomer(customerRecipeDtoRequest.Customer);

                await _recipeService.CreateRecipe(customerRecipeDtoRequest.Recipe, customer.Id);
                await _unitOfWork.SaveChangesAsync();

                return new ApiServiceResponse("El cliente y su receta se han creado correctamente", 201);
            }
            catch (Exception ex)
            {
                return new ApiServiceResponse($"Ocurrió un error: {ex.Message}",500);
            }
        }

        public async Task<ApiServiceResponse> UpdateCustomerRecipeAsync(CustomerRecipeDtoRequest customerRecipeDtoRequest)
        {
            try
            {
                var customer = await _customerService.UpdateCustomer(customerRecipeDtoRequest.Customer);

                await _recipeService.UpdateRecipe(customerRecipeDtoRequest.Recipe);
                await _unitOfWork.SaveChangesAsync();

                return new ApiServiceResponse("El cliente y su receta se han actualizado correctamente", 200);
            }
            catch (Exception ex)
            {
                return new ApiServiceResponse($"Ocurrió un error: {ex.Message}", 500);
            }
        }
    }
}
