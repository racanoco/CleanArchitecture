using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Common;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly StreamerDbContext _streamerDbContext;

        public RepositoryBase(StreamerDbContext streamerDbContext)
        {
            _streamerDbContext = streamerDbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _streamerDbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _streamerDbContext.Set<T>().Where(expression).ToListAsync();
        }

        // Retorna una colección de datos con una determinada condición lógica dentro de query ordenado.
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _streamerDbContext.Set<T>();

            if(disableTracking) 
                query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (expression is not null)
                query = query.Where(expression);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        // Retorna una colección de datos relacionada entre dentidadescon una determinada condición lógica dentro de query ordenado.
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _streamerDbContext.Set<T>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes is not null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (expression is not null)
                query = query.Where(expression);

            if (orderBy is not null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _streamerDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _streamerDbContext.Set<T>().Add(entity);

            await _streamerDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _streamerDbContext.Entry(entity).State = EntityState.Modified;
            await _streamerDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _streamerDbContext.Set<T>().Remove(entity);

            await _streamerDbContext.SaveChangesAsync();
        }



    }
}
