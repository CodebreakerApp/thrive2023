using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherAPI", Version = "v1" });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAPI v1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

string[] summaries = new[]
{
   "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weather", () =>
{
    var weather = Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast(
        DateTime.Now.AddDays(index),
        Random.Shared.Next(-20, 55),
        summaries[Random.Shared.Next(summaries.Length)])).ToArray();
    return Results.Ok(weather);
}).Produces<WeatherForecast[]>(StatusCodes.Status200OK);

app.Run();

public record class WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
