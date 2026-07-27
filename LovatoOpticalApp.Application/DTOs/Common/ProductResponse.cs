using System.Text.Json.Serialization;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.DTOs.Common
{
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
	[JsonDerivedType(typeof(FrameResponseDto), "frame")]
	public class ProductResponse
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal PurchasePrice { get; set; }
		public decimal SalePrice { get; set; }
		public int Quantity { get; set; }
		public int MinimumQuantity { get; set; }
		public ProductTypeEnum Type { get; set; }
        public string Description { get; set; }
    }
}