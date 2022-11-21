using CleanArchitecture.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<TEntity> Repositiry<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}
