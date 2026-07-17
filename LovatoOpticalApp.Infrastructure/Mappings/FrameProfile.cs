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
			CreateMap<FrameRequestDto, Frame>()
				.ForMember(dest => dest.Material, opt => opt.MapFrom(src => ParseMaterial(src.Material)))
				.ForMember(dest => dest.Shape, opt => opt.MapFrom(src => ParseShape(src.Shape)));
				//.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

			CreateMap<Frame, FrameRequestDto>();
		}

		private static FrameMaterialEnum ParseMaterial(string material)
		{
			return material switch
			{
				"Metal" => FrameMaterialEnum.Metal,
				"Acetato" => FrameMaterialEnum.Acetato,
				"Titanio" => FrameMaterialEnum.Titanio,
				"Plastico" or "Plástico" => FrameMaterialEnum.Plastico,
				_ => default
			};
		}

		private static FrameShapeEnum ParseShape(string shape)
		{
			return shape switch
			{
				"Rectangular" => FrameShapeEnum.Rectangular,
				"Circle" or "Redondo" => FrameShapeEnum.Circle,
				"Square" or "Cuadrado" => FrameShapeEnum.Square,
				"Oval" or "Ovalado" => FrameShapeEnum.Oval,
				_ => default
			};
		}
	}
}