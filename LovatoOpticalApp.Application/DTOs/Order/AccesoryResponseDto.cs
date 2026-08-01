using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.DTOs
{
	public class AccesoryResponseDto: ProductResponse
	{
		public bool IsOptional { get; set; }
	}
}