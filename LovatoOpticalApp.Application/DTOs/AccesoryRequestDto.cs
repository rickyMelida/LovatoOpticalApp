using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.DTOs
{
	public class AccessoryRequestDto: ProductRequest
	{
		public bool IsOptional { get; set; }
	}
}