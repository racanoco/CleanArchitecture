using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

// await AddNewRecords("Disney", "www.disney.com");
// QueryStreaming();
// await QueryFilter();
// await QueryMethods();
// await QueryLinq();
// await TrackingAndNotTracking();
// await AddNewStreamerWithVideo();
// await AddNewStreamerWithVideoId();
// await AddNewActorWithVideo();
// await AddNewDirectorWithVideo();
await MultipleEntitiesQuery();

Console.WriteLine("Presione cualquier tecla para temrinar el proceso");
Console.ReadKey();

void QueryStreaming()
{
    var streamers = dbContext?.Streamers!.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
}

async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compañia de streaming:");
    
    string streamerName = Console.ReadLine();

    // var streamers = await dbContext!.Streamers!.Where(x => x.Name == streamerName).ToListAsync();
    var streamers = await dbContext!.Streamers!.Where(x => x.Name.Equals(streamerName)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

    // var streamerPartialResultsa = await dbContext!.Streamers!.Where(x => x.Name.Contains(streamerName)).ToListAsync();

    var streamerPartialResultsa = await dbContext!.Streamers!.Where(x => EF.Functions.Like(x.Name, $"%{streamerName}%")).ToListAsync();

    foreach (var streamer in streamerPartialResultsa)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

}

async Task AddNewRecords(string streamerName, string streamerUrl)
{
    try
    {
        Streamer streamer = new()
        {
            Name = streamerName,
            Url = streamerUrl
        };

        dbContext!.Streamers!.Add(streamer);

        await dbContext.SaveChangesAsync();

        var movies = new List<Video>
{
    new Video
    {
        Name = "X Men 1",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Name = "X Men 2",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Name = "X Men 3",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Name = "X Men 4",
        StreamerId = streamer.Id,
    }
};

        await dbContext.AddRangeAsync(movies);

        await dbContext.SaveChangesAsync();
    }
    catch (Exception)
    {

        throw;
    }    

}

async Task QueryMethods()
{
    var dbContextStreamers = dbContext!.Streamers!;

    // Saltará una excepción si no encuentra resultado.
    var streamerFirstAsync = await dbContextStreamers.Where(y => y.Name.Contains('a')).FirstAsync();

    // Retornará null si no hay resultado.
    var streamer2FirstOrDefaultAsync = await dbContextStreamers.Where(y => y.Name.Contains('a')).FirstOrDefaultAsync();

    // Aplicar directamente el FirstOrDefaultAsync
    var streamer3FirstOrDefaultAsync = await dbContextStreamers.FirstOrDefaultAsync(y => y.Name.Contains('a'));

    var singleAsync = await dbContextStreamers.Where(y => y.Id == 1).SingleAsync();

    var singleOrDefaultAsync = await dbContextStreamers.Where(y => y.Id == 1).SingleOrDefaultAsync();

    var resultado = await dbContextStreamers.FindAsync(1);
}

async Task QueryLinq()
{   

    var streamers = await (from i in dbContext.Streamers select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }

    Console.WriteLine($"Ingrese el servicio de streaming");

    var streamerName = Console.ReadLine();

    var streamersWhere = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Name, $"%{streamerName}%")
                           select i).ToListAsync();

    foreach (var streamer in streamersWhere)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }


}

async Task TrackingAndNotTracking()
{
    // Obtenemos datos para luego poder modificarlos.
    var streamerWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 2);

    // Utilizar cuando no hay que realizar cambios en los registros. AsNoTracking "sin seguimineto" sirve para evitar que EF Core le de seguimiento, esto hará que tu aplicación ejecute queries más rápidamente.
    // Cuando termina libera la memoria.
    var streamerWithNoTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking.Name = "Amazon Prime";

    await dbContext!.SaveChangesAsync();

}

async Task AddNewStreamerWithVideo()
{
    var pantaya = new Streamer
    {
        Name = "Pantaya"
    };

    var hungerGames = new Video
    {
        Name = "Hunger Games",
        Streamer = pantaya
    };

    await dbContext.AddAsync(hungerGames);
    await dbContext!.SaveChangesAsync();
}

async Task AddNewStreamerWithVideoId()
{
    var batmanForever = new Video
    {
        Name = "Batman Forever",
        StreamerId = 4
    };

    await dbContext.AddAsync(batmanForever);
    await dbContext!.SaveChangesAsync();

}

async Task AddNewActorWithVideo()
{
    var actor = new Actor
    {
        Name = "Brad",
        LastName = "Pitt"
    };

    await dbContext.AddAsync(actor);
    await dbContext!.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext.AddAsync(videoActor);
    await dbContext!.SaveChangesAsync();
}

async Task AddNewDirectorWithVideo()
{
    var director = new Director()
    {
        Name = "Raúl",
        LastName = "Cano Corbera",
        VideoId = 1
    };

    await dbContext.AddAsync(director);
    await dbContext.SaveChangesAsync();
}

async Task MultipleEntitiesQuery()
{
    //var videosWithActores = await dbContext!.Videos!.Include(q => q.Actors).FirstOrDefaultAsync(q => q.Id == 1);

    //var actor = await dbContext!.Actores!.Select(q => q.Name).ToListAsync();
    try
    {

        // Muestra todas las peliculas si contiene director o no.
        var videoWithDirector = await dbContext!.Videos!                            
                            .Include(q => q.Director)
                            .Select(q =>
                               new
                               {
                                   Director_Nombre_Completo = $"{q.Director.Name} {q.Director.LastName}",
                                   Movie = q.Name
                               }
                             )
                            .ToListAsync();

        //// Muestra peliculas si contiene director
        //var videoWithDirector = await dbContext!.Videos!
        //                    .Where(q => q.Director != null)
        //                    .Include(q => q.Director)
        //                    .Select(q =>
        //                       new
        //                       {
        //                           Director_Nombre_Completo = $"{q.Director.Name} {q.Director.LastName}",
        //                           Movie = q.Name
        //                       }
        //                     )
        //                    .ToListAsync();


        foreach (var pelicula in videoWithDirector)
        {
            Console.WriteLine($"{pelicula.Movie}  - {pelicula.Director_Nombre_Completo} ");
        }

    }
    catch (Exception ex)
    {

        throw;
    }
    
}


