using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities.Payments
{
    public class DebitCardPayment : IPayment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public decimal Amount { get; private set; }
        public string Bank { get; private set; }
        public string LastFourDigits { get; private set; }
        public DateTime PaidAt { get; private set; } = DateTime.UtcNow;
        [NotMapped]
        public PaymentMethodEnum Method => PaymentMethodEnum.DebitCard;

        private DebitCardPayment() { }

        public DebitCardPayment(decimal amount, string bank, string lastFourDigits)
        {
            Amount = amount;
            Bank = bank;
            LastFourDigits = lastFourDigits;
        }
    }
}
