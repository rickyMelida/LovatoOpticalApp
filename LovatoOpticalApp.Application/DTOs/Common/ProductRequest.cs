using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Application.DTOs.Common
{
	public class ProductRequest
	{
		public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public int MinimumQuantity { get; set; }
		public ProductTypeEnum Type { get; set; }
		public string Description { get; set; }
	}
}