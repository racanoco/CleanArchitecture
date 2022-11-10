using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(StreamerDbContext streamerDbContext) : base(streamerDbContext)
        {
        }

        public async Task<Video> GetVideoByName(string videoName)
        {
            return await _streamerDbContext.Videos!.Where(x => x.Name == videoName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Video>> GetVideoByUserName(string userName)
        {
            return await _streamerDbContext.Videos!.Where(x => x.CreatedBy == userName).ToListAsync();
        }
    }
}
