using LovatoOpticalApp.Application.DTOs.Common;
using LovatoOpticalApp.Core.Entities.Enums;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Application.DTOs
{
	public class FrameResponseDto: ProductResponse
	{
		public string Code { get; set; } = string.Empty;
		public FrameMaterialEnum Material { get; set; }
		public FrameTypeEnum FrameType { get; set; }
		public string Color { get; set; } = string.Empty;
		public decimal Price => SalePrice;
		public DateTime CreatedAt { get; private set; }
	}
}