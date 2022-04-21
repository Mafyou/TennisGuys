using APIForTennis.Models;
using APIForTennis.Helpers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

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

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}