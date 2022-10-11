using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

// await AddNewRecords("Disney", "www.disney.com");

// QueryStreaming();

// await QueryFilter();

await QueryMethods();

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

