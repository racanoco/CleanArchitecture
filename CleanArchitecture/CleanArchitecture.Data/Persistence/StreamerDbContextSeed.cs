using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext streamerDbContext, ILogger<StreamerDbContextSeed> logger)
        {
            if(!streamerDbContext.Streamers!.Any())
                streamerDbContext.Streamers!.AddRange(GetPreconfiguredStreamer());
                await streamerDbContext.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {streamerDbContext}", typeof(StreamerDbContext).Name);
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer {CreatedBy = "raul cano", Name = "HBP", Url = "http://wwww.hbp.com"},
                new Streamer {CreatedBy = "raul cano", Name = "Amazon VIP", Url = "http://wwww.amazonvip.com"},
            };
        }
    }
}
