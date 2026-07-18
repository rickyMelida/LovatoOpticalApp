using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.DTOs.Common
{
	public class ProductResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public string Color { get; set; } = string.Empty;
		public ProductTypeEnum Type { get; set; }
	}
}