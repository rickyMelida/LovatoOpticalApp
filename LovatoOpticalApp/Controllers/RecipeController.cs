using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class RecipeController : Controller
    {
        protected readonly ICustomerService _customerService;
        protected readonly ICustomerRecipeUnitOfWork _customerRecipeUnitOfWork;
        protected readonly IRecipeService _recipeService;

        public RecipeController(ICustomerService customerService, ICustomerRecipeUnitOfWork customerRecipeUnitOfWork, IRecipeService recipeService)
        {
            _customerService = customerService;
            _customerRecipeUnitOfWork = customerRecipeUnitOfWork;
            _recipeService = recipeService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiServiceResponse>> CreateRecipe([FromBody] RecipeRequestDto recipeRequest)
        {
            try
            {
                if (recipeRequest.CustomerId == Guid.Empty)
                    return BadRequest("El ID del cliente es requerido");

                await _recipeService.CreateRecipe(recipeRequest, recipeRequest.CustomerId, true);

                return Ok(new ApiServiceResponse("Receta Agregada Correctamente", 201));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<RecipeResponseDto>> GetLastRecipe(string customerId)
        {
            if (String.IsNullOrEmpty(customerId) || !Guid.TryParse(customerId, out Guid parsedCustomerId))
                return BadRequest("El ID del cliente no es válido.");

            var result = await _recipeService.GetLastRecipe(customerId);  

            return Ok(result);
        }
    }
}
