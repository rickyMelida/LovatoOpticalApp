using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Core.Entities
{
    public class Crystal : Product
	{
		public Crystal(
			string name,
			decimal purchasePrice,
			decimal salePrice,
			int quantity,
			int minimumQuantity = 1,
			string description = "")
		{
			Type = ProductTypeEnum.Crystal;  // se asigna aquí
			Name = name;
			PurchasePrice = purchasePrice;
			SalePrice = salePrice;
			Quantity = quantity;
			MinimumQuantity = minimumQuantity;
			Description = description;
		}
	}
}
