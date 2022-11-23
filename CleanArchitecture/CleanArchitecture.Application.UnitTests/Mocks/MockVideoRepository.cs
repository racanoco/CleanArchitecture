using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static void AddDataVideoRepository( StreamerDbContext streamerDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior()); // Evitar error "AutoFixture.ObjectCreationExceptionWithPath" al ejecutar un método de prueba.

            var videos = fixture.CreateMany<Video>().ToList();            
            
            // Agrega un nuevo registro de prueba creado por el UserName "racanoco" en la entidad Video.
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "racanoco")
                .Create()
                );                       

            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();            
        }
    }
}
