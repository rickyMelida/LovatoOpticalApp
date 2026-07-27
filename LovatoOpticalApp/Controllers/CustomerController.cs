using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CustomerController : RecipeController
    {
      
        public CustomerController(ICustomerService customerService, ICustomerRecipeUnitOfWork customerRecipeUnitOfWork, IRecipeService recipeService): 
            base(customerService, customerRecipeUnitOfWork, recipeService)
        {
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
    }
}
