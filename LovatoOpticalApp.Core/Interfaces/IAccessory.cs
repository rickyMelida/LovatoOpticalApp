namespace LovatoOpticalApp.Core.Interfaces
{
    public interface IAccessory: IProduct
    {
        bool IsOptional { get; }
    }
}
