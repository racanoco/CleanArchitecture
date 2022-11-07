using CleanArchitecture.Common;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //    dbContextOptionsBuilder.UseSqlServer("data source=.\\ANBU;initial catalog=StreamerDB;user id=sa;password=ps015-j;persist security info=True;")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        //        .EnableSensitiveDataLogging();
        //}

        /// <summary>
        /// Valores por defecto Propiedades BaseDomainModel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (item.State)
                {   
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.Now;
                        item.Entity.CreatedBy = "system";
                        break;
                    case EntityState.Modified:
                        item.Entity.LastModifiedDate = DateTime.Now;
                        item.Entity.LastModifiedBy = "system";
                        break;                   
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API

            // Relación de 1 a muchos.
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relación de muchos a muchos.
            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actors)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );

        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Actor>? Actores { get; set; }
        public DbSet<Director>? Director { get; set; }
    }
}
