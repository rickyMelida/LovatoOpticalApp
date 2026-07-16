using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities
{
    public class Accessory : IAccessory
    {
        public bool IsOptional {  get; private set; }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SalePrice { get; private set; }

        public int Quantity { get; private set; }

        public int MinimumQuantity { get; private set; }

        public Accessory(string name, decimal purchasePrice, decimal salePrice, bool isOptional, int quantity, int minimumQuantity)
        {
            Name = name;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            IsOptional = isOptional;
            Quantity = quantity;
            MinimumQuantity = minimumQuantity;
        }
    }
}
