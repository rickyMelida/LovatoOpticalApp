using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Mappings
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResquestDto>();
            CreateMap<CustomerResquestDto, Customer>();
        }
    }
}
