using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities
{
    public class GlassesCase : Accessory
    {
        public bool IsOptional { get; private set; }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public int MinimumQuantity { get; private set; }

        public string Name { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SalePrice { get; private set; }

        public int Quantity { get; private set; } = 0;

        public GlassesCase(string name, decimal purchasePrice, decimal salePrice, bool isOptional, int minimumQuantity)
            : base(name, purchasePrice, salePrice, isOptional, minimumQuantity, 0)
        {
            Name = name;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            IsOptional = isOptional;
            MinimumQuantity = minimumQuantity;
        }
    }
}
