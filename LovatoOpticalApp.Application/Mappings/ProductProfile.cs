using AutoMapper;
using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Mappings
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductResponse>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
		}
	}
}