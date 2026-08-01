using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Mappings
{
	public class AccessoryProfile : Profile
	{
		public AccessoryProfile()
		{
			CreateMap<AccessoryRequestDto, Accessory>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
			CreateMap<Accessory, AccessoryRequestDto>();
			CreateMap<Accessory, AccesoryResponseDto>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
		}
	}
}