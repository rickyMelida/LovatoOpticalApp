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
			CreateMap<CrystalPrice, CrystalPriceDto>();
			CreateMap<CrystalTypeDto, CrystalType>();
			CreateMap<CrystalType, CrystalTypeDto>();
		}
	}
}