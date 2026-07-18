using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.DTOs
{
	public class FrameRequestDto
	{
		public ProductTypeEnum Type { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Material { get; set; }
		public string FrameType { get; set; }
		public string Color { get; set; }
		public decimal PurchasePrice { get; set; }
		public decimal SalePrice { get; set; }
		public int Quantity { get; set; }
		public int MinimumQuantity { get; set; }
		public Guid? CreatedBy { get; set; }
		public string Description { get; set; }
	}
}