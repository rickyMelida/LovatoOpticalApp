using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRecipeUnitOfWork _customerRecipeUnitOfWork;
        private readonly IRecipeService _recipeService;
        public CustomerController(ICustomerService customerService, ICustomerRecipeUnitOfWork customerRecipeUnitOfWork, IRecipeService recipeService    )
        {
            _customerService = customerService;
            _customerRecipeUnitOfWork = customerRecipeUnitOfWork;
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            var parameters = new PaginationParams { PageNumber = 1, PageSize = 10 };

            var customers = await _customerService.GetCustomers(parameters);
            ViewData["customers"] = customers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRecipeDtoRequest customerResquest)
        {
            var result = await _customerRecipeUnitOfWork.CreateCustomerRecipeAsync(customerResquest);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomerDetails(string customerId)
        {
            if (String.IsNullOrEmpty(customerId) || !Guid.TryParse(customerId, out Guid parsedCustomerId))
                return BadRequest("El ID del cliente no es válido.");

            var result = await _customerService.GetCustomerById(parsedCustomerId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CustomerRecipeDtoRequest customerResquest)
        {
            var result = await _customerRecipeUnitOfWork.UpdateCustomerRecipeAsync(customerResquest);

            return Ok(result);
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

    }
}
