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
			CreateMap<Frame, FrameResponseDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.PurchasePrice, opt => opt.MapFrom(src => src.PurchasePrice))
				.ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
				.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(dest => dest.MinimumQuantity, opt => opt.MapFrom(src => src.MinimumQuantity))
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
				.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
				.ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
				.ForMember(dest => dest.FrameType, opt => opt.MapFrom(src => src.FrameType))
				.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));
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