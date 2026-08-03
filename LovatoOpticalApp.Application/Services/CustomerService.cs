using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerResponseDto> CreateCustomer(CustomerResquestDto customerRequestDto, bool createOnlyCustomer = false)
        {
            var customer = _mapper.Map<Customer>(customerRequestDto);
            var result = await _customerRepository.CreateCustomer(customer);

			if(createOnlyCustomer)
				await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerResponseDto>(result);
        }

        public async Task DeleteCustomer(Guid customerId)
        {
            await _customerRepository.DeleteCustomer(customerId);
        }

        public async Task<CustomerResponseDto> GetCustomerByDoc(string doc)
        {
            string docCleaned = doc.Trim().ToLower();
            var customer = await _customerRepository.GetCustomerByDoc(docCleaned);

            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public async Task<CustomerResponseDto> GetCustomerById(Guid customerId)
        {
            var customer = await _customerRepository.GetCustomerDetails(customerId);

            return _mapper.Map<CustomerResponseDto>(customer);
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
                    Email = f.Email,
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

        public async Task<PagedResult<CustomerResponseDto>> SearchCustomer(string query, PaginationParams parameters)
        {
            var customers = await _customerRepository.SearchCustomer(query);
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
                    Email = f.Email,
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

        public async Task<CustomerResponseDto> UpdateCustomer(CustomerResquestDto customerRequestDto)
        {
            var customer = _mapper.Map<Customer>(customerRequestDto);
            var result = await _customerRepository.UpdateCustomer(customer);

            return _mapper.Map<CustomerResponseDto>(result);
        }
    }
}
