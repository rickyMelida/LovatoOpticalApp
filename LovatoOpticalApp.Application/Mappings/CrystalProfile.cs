using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Mappings
{
	public class CrystalProfile: Profile
	{
		public CrystalProfile()
		{
			CreateMap<CrystalPriceDto, CrystalPrice>();
			CreateMap<CrystalPrice, CrystalPriceDto>()
				.ForMember(destination => destination.CrystalType, options => options.Ignore());
			CreateMap<CrystalTypeDto, CrystalType>();
			CreateMap<CrystalType, CrystalTypeDto>();
			CreateMap<CrystalBrand, CrystalBrandDto>();
			CreateMap<CrystalBrandPrice, CrystalBrandPriceDto>();
		}
	}
}