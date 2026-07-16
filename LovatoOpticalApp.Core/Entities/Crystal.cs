using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities
{
    public class Crystal: IProduct
    {
        public string TechnicalCharacteristics { get; set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SalePrice { get; private set; }

        public int Quantity { get; private set; } = 0;

        public int MinimumQuantity { get; private set; }

        public Crystal(string name, string technicalCharacteristics, decimal purchasePrice, decimal salePrice, int quantity, int minimumQuantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            TechnicalCharacteristics = technicalCharacteristics;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Quantity = quantity;
            MinimumQuantity = minimumQuantity;
        }
    }
}
