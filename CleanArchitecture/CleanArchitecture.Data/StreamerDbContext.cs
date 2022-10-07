using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("data source=.\\ANBU;initial catalog=StreamerDB;user id=sa;password=ps015-j;persist security info=True;")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name}, LogLevel.Information )
                .EnableSensitiveDataLogging();
        }
        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
    }
}
