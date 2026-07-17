using LovatoOpticalApp.Core.Interfaces;

namespace LovatoOpticalApp.Core.Entities.Discounts
{
    public class DiscountByPercentage : IDiscount
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Percentage { get; private set; }

        private DiscountByPercentage() { }

        public DiscountByPercentage(decimal percentage, string description = "")
        {
            if (percentage <= 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "El porcentaje debe estar entre 0 y 100.");

            Id = Guid.NewGuid();
            Percentage = percentage;
            Description = string.IsNullOrWhiteSpace(description)
                ? $"Descuento {percentage}%"
                : description;
        }

        public decimal Calculate(decimal subTotal) => Math.Round(subTotal * Percentage / 100, 2);
    }
}
