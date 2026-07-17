using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities.Payments
{
    public class CreditCardPayment : IPayment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public decimal Amount { get; private set; }
        public int Installments { get; private set; }
        [NotMapped]
        public decimal InstallmentValue => Math.Round(Amount / Installments, 2);
        public string CardBrand { get; private set; }
        public DateTime PaidAt { get; private set; } = DateTime.UtcNow;
        [NotMapped]
        public PaymentMethodEnum Method => PaymentMethodEnum.CreditCard;

        private CreditCardPayment() { }

        public CreditCardPayment(decimal amount, int installments, string cardBrand = "")
        {
            if (installments < 1)
                throw new ArgumentOutOfRangeException(nameof(installments), "El número de cuotas debe ser al menos 1.");

            Amount = amount;
            Installments = installments;
            CardBrand = cardBrand;
        }
    }
}
