using LovatoOpticalApp.Core.Enums;
using LovatoOpticalApp.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LovatoOpticalApp.Core.Entities.Payments
{
    public class TransferPayment : IPayment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public decimal Amount { get; private set; }
        public PaymentProof Proof { get; private set; }
        [NotMapped]
        public bool IsConfirmed => Proof.IsVerified;
        public DateTime PaidAt { get; private set; } = DateTime.UtcNow;
        [NotMapped]
        public PaymentMethodEnum Method => PaymentMethodEnum.BankTransfer;

        private TransferPayment() { }

        public TransferPayment(decimal amount, PaymentProof proof)
        {
            Amount = amount;
            Proof = proof ?? throw new ArgumentNullException(nameof(proof));
        }
    }
}
