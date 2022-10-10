using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext dbContext = new();

// await AddNewRecords("Disney", "www.disney.com");

QueryStreaming();


void QueryStreaming()
{
    var streamers = dbContext?.Streamers!.ToList();

    foreach (var streamer in streamers)
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