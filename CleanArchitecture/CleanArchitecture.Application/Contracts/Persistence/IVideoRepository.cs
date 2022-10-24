using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    /// <summary>
    /// Interfaz donde irán los métodos personalizados para el mantenimiento de la entidad Video.
    /// Hereda los métodos genéricos de la clase IAsyncRepository
    /// </summary>
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string videoName);
        Task<IEnumerable<Video>> GetVideoByUserName(string userName);
    }
}
