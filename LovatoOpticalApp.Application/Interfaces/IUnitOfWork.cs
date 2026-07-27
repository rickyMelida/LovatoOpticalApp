namespace LovatoOpticalApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
