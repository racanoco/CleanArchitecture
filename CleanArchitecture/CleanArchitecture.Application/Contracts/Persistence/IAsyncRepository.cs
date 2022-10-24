using CleanArchitecture.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    /// <summary>
    /// Interfaz con métodos genéricos para el mantenimiento de las entidades.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T>where T : BaseDomainModel
    {
        // Retorna una lista de todos los registros de una entidad determinada.
        Task<IReadOnlyList<T>> GetAllAsync();

        // Retorna una colección de datos con una determinada condición lógica dentro de query.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> expression);

        // Retorna una colección de datos con una determinada condición lógica dentro de query ordenado.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true);

        // Retorna una colección de datos relacionada entre dentidadescon una determinada condición lógica dentro de query ordenado.
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true);
        
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
