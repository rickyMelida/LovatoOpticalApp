using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Application.Mappings
{
	public class CrystalMappingProfile : Profile
	{
		public CrystalMappingProfile()
		{
			CreateMap<CrystalDtoRequest, Crystal>();
			CreateMap<Crystal, CrystalDtoResponse>();
		}
	}
}