using APIForTennis.Helpers;
using APIForTennis.Models;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Les mecs du tennis", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var players = initialize();

app.MapGet("/players", () =>
{
    return players.OrderBy(x => x.MyLeaderBoard.Rank);
});
app.MapPost("/playerId", (int id) =>
{
    return players.SingleOrDefault(x => x.Id == id);
});
app.MapGet("/Stats", () =>
{
    var part1 = PartOne();

    var part2 = PartTwo();

    var part3 = PartThree();

    return new { part1, part2, part3};
});
app.Run();

List<Players> initialize()
{
    try
    {
        dynamic? json = JsonConvert.DeserializeObject(File.ReadAllText(Directory.GetCurrentDirectory() + "/Datas/headtohead.json"));
        var playersJson = JsonConvert.SerializeObject(json?.players);
        return JsonConvert.DeserializeObject<List<Players>>(playersJson);
    }
    catch (Exception ex)
    {
        throw;
    }
}

object PartOne()
{
    var countryWithPoints = players.Select(x => new
    {
        x.MyLeaderBoard.Points,
        x.MyCountry
    });
    return countryWithPoints.First(x => x.Points == countryWithPoints.Max(y => y.Points));
}

List<PlayersWithIMC> PartTwo()
{
    var playersWithIMC = new List<PlayersWithIMC>(players.Count - 1);
    foreach (var player in players)
    {
        playersWithIMC.Add(new PlayersWithIMC { Name = player.ShortName, IMC = player.MyLeaderBoard.Weight * 10 / Math.Pow(player.MyLeaderBoard.Height, 2) });
    }
    return playersWithIMC;
}

double PartThree()
{
    var playersSize = players.Select(x => new PlayersWithMediane { Name = x.ShortName, Size = x.MyLeaderBoard.Height }).ToList();

    double mid = (playersSize.Count - 1) / 2.0;
    return playersSize[(int)mid].AddMediane(playersSize[(int)(mid + 0.5)]) / 2;
}

record PlayersWithIMC
{
    public string? Name { get; set; }
    public double IMC { get; set; }
}
public record PlayersWithMediane
{
    public string? Name { get; set; }
    public double Size { get; set; }
}