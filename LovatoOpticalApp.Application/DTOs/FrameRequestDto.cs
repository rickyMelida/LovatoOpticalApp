using System.ComponentModel.DataAnnotations;
using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.DTOs
{
	public class FrameRequestDto
	{
		public string? Id { get; set; }
		public ProductTypeEnum Type { get; set; }
		[Required] public string Name { get; set; }
		[Required] public string Code { get; set; }
		[Required] public string Material { get; set; }
		[Required] public string FrameType { get; set; }
		[Required] public string Color { get; set; }
		public decimal PurchasePrice { get; set; }
		public decimal SalePrice { get; set; }
		public int Quantity { get; set; }
		public int MinimumQuantity { get; set; }
		public Guid? CreatedBy { get; set; }
		public string? Description { get; set; }
	}
}