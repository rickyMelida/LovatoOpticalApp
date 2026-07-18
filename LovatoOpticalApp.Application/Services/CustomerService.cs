using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        public CustomerService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<ApiServiceResponse> CreateCustomer(CustomerResquestDto customerRequestDto)
        {
            var customer = _mapper.Map<Customer>(customerRequestDto);

            throw new NotImplementedException();
        }

        public Task<ApiServiceResponse> DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiServiceResponse> UpdateCustomer(CustomerResquestDto customerRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
