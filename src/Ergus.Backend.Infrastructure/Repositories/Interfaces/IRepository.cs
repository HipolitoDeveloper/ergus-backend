namespace Ergus.Backend.Infrastructure.Repositories.Interfaces
{
    public interface IRepository : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
