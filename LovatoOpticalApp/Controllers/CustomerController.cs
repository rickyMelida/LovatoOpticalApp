using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovatoOpticalApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var parameters = new PaginationParams { PageNumber = 1, PageSize = 10 };

            var customers = await _customerService.GetCustomers(parameters);
            ViewData["customers"] = customers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerResquestDto customerResquest)
        {
            var result = await _customerService.CreateCustomer(customerResquest);

            return Ok(result);
        }
    }
}
