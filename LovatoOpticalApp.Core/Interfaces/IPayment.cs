using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Core.Interfaces
{
    public interface IPayment
    {
        Guid Id { get; }
        decimal Amount { get; }
        DateTime PaidAt { get; }
        PaymentMethodEnum Method { get; }
    }
}
