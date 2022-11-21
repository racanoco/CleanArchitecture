using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDbContext _streamerDbContext;

        public UnitOfWork(StreamerDbContext streamerDbContext)
        {
            _streamerDbContext = streamerDbContext;
        }

        public async Task<int> Complete()
        {
            return await _streamerDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _streamerDbContext.Dispose();
        }

        public IAsyncRepository<TEntity> Repositiry<TEntity>() where TEntity : BaseDomainModel
        {
            if(_repositories is null) 
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type)) 
            { 
                var repositoryType = typeof(IAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _streamerDbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
