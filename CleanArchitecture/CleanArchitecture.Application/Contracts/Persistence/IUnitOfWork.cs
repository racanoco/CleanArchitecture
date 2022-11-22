using CleanArchitecture.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IStreamerRepository StreamerRepository { get; }
        IVideoRepository VideoRepository { get; }
        IAsyncRepository<TEntity> Repositiry<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
    }
}
