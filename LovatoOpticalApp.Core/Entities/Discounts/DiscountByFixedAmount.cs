using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities.Discounts
{
    public class DiscountByFixedAmount : IDiscount
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal FixedAmount { get; private set; }

        private DiscountByFixedAmount() { }

        public DiscountByFixedAmount(decimal fixedAmount, string description = "")
        {
            if (fixedAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(fixedAmount), "El monto del descuento debe ser mayor a cero.");

            Id = Guid.NewGuid();
            FixedAmount = fixedAmount;
            Description = string.IsNullOrWhiteSpace(description)
                ? $"Descuento fijo ${fixedAmount:0.00}"
                : description;
        }

        public decimal Calculate(decimal subTotal) => Math.Min(FixedAmount, subTotal);
    }
}
