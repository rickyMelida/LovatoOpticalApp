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
			CreateMap<string, FrameShapeEnum>().ConvertUsing(src => ParseShape(src));

			CreateMap<FrameRequestDto, Frame>()
				.ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Material))
				.ForMember(dest => dest.Shape, opt => opt.MapFrom(src => src.Shape));

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

		private static FrameShapeEnum ParseShape(string? shape)
		{
			if (string.IsNullOrWhiteSpace(shape))
				return default;

			return shape.Trim().ToLowerInvariant() switch
			{
				"rectangular" or "rectangulares" => FrameShapeEnum.Rectangular,
				"circle" or "redondo" or "redonda" or "circular" => FrameShapeEnum.Circle,
				"square" or "cuadrado" or "cuadrada" => FrameShapeEnum.Square,
				"oval" or "ovalado" or "ovalada" => FrameShapeEnum.Oval,
				_ => default
			};
		}
	}
}