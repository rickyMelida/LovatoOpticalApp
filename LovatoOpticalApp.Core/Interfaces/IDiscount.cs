namespace LovatoOpticalApp.Core.Interfaces
{
    public interface IDiscount
    {
        string Description { get; }
        decimal Calculate(decimal subTotal);
    }
}
