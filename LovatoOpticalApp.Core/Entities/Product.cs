using LovatoOpticalApp.Core.Entities.Enums;

namespace LovatoOpticalApp.Core.Entities
{
    public abstract class Product : IProduct
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Name { get; protected set; } = string.Empty;
        public decimal PurchasePrice { get; protected set; }
        public decimal SalePrice { get; protected set; }
        public int Quantity { get; protected set; }
        public int MinimumQuantity { get; protected set; } = 1;
        public ProductTypeEnum Type { get; protected set; }

        // Lógica común centralizada (evita duplicarla en Frame, Crystal, etc.)
        public bool HasStock() => Quantity > 0;
        public bool IsBelowMinimumStock() => Quantity < MinimumQuantity;

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");
            Quantity = quantity;
        }
    }
}