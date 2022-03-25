namespace Ergus.Backend.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<int> CommitReturningAggregateRootId();
        void ReloadContext();
    }
}
