using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Application.Mappings
{
	public class FrameProfile : Profile
	{
		public FrameProfile()
		{
			CreateMap<string, FrameMaterialEnum>().ConvertUsing(src => ParseMaterial(src));
			CreateMap<string, FrameTypeEnum>().ConvertUsing(src => ParseFrameType(src));

			CreateMap<FrameRequestDto, Frame>()
				.ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
				.ForMember(dest => dest.FrameType, opt => opt.MapFrom(src => src.FrameType));

			CreateMap<Frame, FrameRequestDto>();
		}

		private static FrameMaterialEnum ParseMaterial(string? material)
		{
			if (string.IsNullOrWhiteSpace(material))
				return default;

			return material.Trim().ToLowerInvariant() switch
			{
				"metal" => FrameMaterialEnum.Metal,
				"acetato" => FrameMaterialEnum.Acetato,
				"titanio" => FrameMaterialEnum.Titanio,
				"plastico" or "plástico" or "plastic" => FrameMaterialEnum.Plastico,
				_ => default
			};
		}

		private static FrameTypeEnum ParseFrameType(string? frameType)
		{
			if (string.IsNullOrWhiteSpace(frameType))
				return default;

			return frameType.Trim().ToLowerInvariant() switch
			{
				"hilo" => FrameTypeEnum.Hilo,
				"al tornillo" => FrameTypeEnum.AlTornillo,
				"aro completo" => FrameTypeEnum.AroCompleto,
				_ => default
			};
		}
	}
}