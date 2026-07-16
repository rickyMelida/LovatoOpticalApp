namespace LovatoOpticalApp.Core
{
    public interface IProduct
    {
        Guid Id { get; }
        string Name { get; }
        decimal PurchasePrice { get; }
        decimal SalePrice { get; }
        int Quantity { get; }
        int MinimumQuantity { get; }
    }
}
