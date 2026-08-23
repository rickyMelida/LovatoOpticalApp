using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CustomerController : RecipeController
    {
      
        public CustomerController(ICustomerService customerService, ICustomerRecipeUnitOfWork customerRecipeUnitOfWork, IRecipeService recipeService): 
            base(customerService, customerRecipeUnitOfWork, recipeService)
        {
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var paginationParams = new PaginationParams
			{
				PageNumber = pageNumber > 0 ? pageNumber : 1,
				PageSize = pageSize > 0 ? pageSize : 10
			};

            var customers = await _customerService.GetCustomers(paginationParams);
            ViewData["customers"] = customers;

            return View();
        }

        [HttpPost]
		[Route("Customer/create-customer-recipe")]
        public async Task<IActionResult> CreateCustomerAndRecipe([FromBody] CustomerRecipeDtoRequest customerResquest)
        {
            var result = await _customerRecipeUnitOfWork.CreateCustomerRecipeAsync(customerResquest);

            return Ok(result);
        }

		[HttpPost]
		[Route("Customer/create-customer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerResquestDto customerResquest)
        {
            var result = await _customerService.CreateCustomer(customerResquest, true);

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

        [HttpGet]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomerByDoc(string doc)
        {
            if (String.IsNullOrEmpty(doc))
                return BadRequest("El documento del cliente no es válido.");

            var result = await _customerService.GetCustomerByDoc(doc);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchCustomer(string query)
        {
            var parameters = new PaginationParams { PageNumber = 1, PageSize = 10 };
           
            var customers = String.IsNullOrEmpty(query) 
                    ? await _customerService.GetCustomers(parameters) 
                    : await _customerService.SearchCustomer(query, parameters);

            ViewData["customers"] = customers;

            return PartialView("Grid/_CustomerGrid");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CustomerRecipeDtoRequest customerResquest)
        {
            var result = await _customerRecipeUnitOfWork.UpdateCustomerRecipeAsync(customerResquest);

            return Ok(result);
        }

		[HttpGet]
		public async Task<IActionResult> DeleteCustomer(string customerId)
		{
			if(String.IsNullOrEmpty(customerId) || !Guid.TryParse(customerId, out Guid parseCustomerID))
				return BadRequest("El id del cliente no es valido");

			var result = await _customerRecipeUnitOfWork.DeleteCustomerRecipeAsync(parseCustomerID);

			return Ok(result);
		}
    }
}
