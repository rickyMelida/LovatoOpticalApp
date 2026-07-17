using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities.Payments
{
    public class CashPayment : IPayment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public decimal Amount { get; private set; }
        public decimal AmountReceived { get; private set; }
        [NotMapped]
        public decimal Change => Math.Max(0, AmountReceived - Amount);
        public DateTime PaidAt { get; private set; } = DateTime.UtcNow;
        [NotMapped]
        public PaymentMethodEnum Method => PaymentMethodEnum.Cash;

        private CashPayment() { }

        public CashPayment(decimal amount, decimal amountReceived)
        {
            if (amountReceived < amount)
                throw new ArgumentException("El monto entregado no puede ser menor al monto a pagar.");

            Amount = amount;
            AmountReceived = amountReceived;
        }
    }
}
