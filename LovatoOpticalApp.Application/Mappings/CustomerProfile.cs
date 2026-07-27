using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;

internal static class DateTimeMappingExtensions
{
    public static DateTime ToUtcDateTime(this DateTime value)
        => value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
}

namespace LovatoOpticalApp.Application.Mappings
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResquestDto>();
            CreateMap<CustomerResquestDto, Customer>()
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay.HasValue ? src.BirthDay.Value.ToUtcDateTime() : (DateTime?)null))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<CustomerResponseDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<Recipe, RecipeRequestDto>();
            CreateMap<RecipeRequestDto, Recipe>()
                .ForMember(dest => dest.PrescriptionIssueDate, opt => opt.MapFrom(src => src.PrescriptionIssueDate.ToUtcDateTime()));
            CreateMap<Recipe, RecipeResponseDto>();
        }
    }
}
