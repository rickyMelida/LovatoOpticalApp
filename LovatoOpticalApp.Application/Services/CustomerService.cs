using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using static Azure.Core.HttpHeader;

namespace LovatoOpticalApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<ApiServiceResponse> CreateCustomer(CustomerResquestDto customerRequestDto)
        {
            var customer = _mapper.Map<Customer>(customerRequestDto);
            await _customerRepository.CreateCustomer(customer);

            return new ApiServiceResponse("Cliente agregado exitosamente", 200);
        }

        public Task<ApiServiceResponse> DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<CustomerResponseDto>> GetCustomers(PaginationParams parameters)
        {
            var customers = await _customerRepository.GetCustomers();
            var pageNumber = parameters.PageNumber > 0 ? parameters.PageNumber : 1;
            var pageSize = parameters.PageSize > 0 ? parameters.PageSize : 10;
            var totalCount = customers.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var safePageNumber = Math.Min(pageNumber, Math.Max(totalPages, 1));
            var skip = (safePageNumber - 1) * pageSize;

            var pagedItems = customers
                .Skip(skip)
                .Take(pageSize)
                .Select(f => new CustomerResponseDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    CiRuc = f.CiRuc,
                    BirthDay = f.BirthDay,
                    Address = f.Address,
                    Phone = f.Phone,
                    CreationDate = f.CreationDate
                })
                .ToList();

            return new PagedResult<CustomerResponseDto>
            {
                Items = pagedItems,
                TotalCount = totalCount,
                PageNumber = safePageNumber,
                PageSize = pageSize
            };
        }

        public Task<ApiServiceResponse> UpdateCustomer(CustomerResquestDto customerRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
