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

        public IActionResult Index()
        {
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
