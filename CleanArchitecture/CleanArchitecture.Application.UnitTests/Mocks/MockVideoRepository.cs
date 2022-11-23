using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<VideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior()); // Evitar error "AutoFixture.ObjectCreationExceptionWithPath" al ejecutar un método de prueba.
            var videos = fixture.CreateMany<Video>().ToList();

            // Pasar a una funcion privada.
            // INICIO---Configuración de la base de datos en memoria utilizamos el paquete EntityFrameworkCore.InMemory.---------------------------------------------
            // Agrega un nuevo registro de prueba creado por el UserName "racanoco" en la entidad Video.
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "racanoco")
                .Create()
                );

            // Configuración de la base de datos en memoria, 
            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase(databaseName: $"NewInstanceStreamerDbContextFake-{Guid.NewGuid()}")
                .Options;

            // Creando la instancia del entity framework
            var streamerDbContextFake = new StreamerDbContext(options);
            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();
            // FIN-------------------------------------------------------------------------------------------------

            var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);          

            return mockRepository;
        }
    }
}
