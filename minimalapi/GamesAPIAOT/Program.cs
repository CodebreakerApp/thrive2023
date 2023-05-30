using Microsoft.AspNetCore.Http.Json;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//  https://github.com/dotnet/runtime / issues / 73124
//builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    // obsolete:
    // options.SerializerOptions.AddContext<AppJsonSerializerContext>();
});

//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
//    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});

builder.Services.AddSingleton<IGamesRepository, InMemoryGamesRepository>();
builder.Services.AddSingleton<GamesFactory>();
builder.Services.AddTransient<IGamesService, GamesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.MapGameEndpoints(app.Logger);

app.Run();


[JsonSerializable(typeof(Game[]))]
[JsonSerializable(typeof(CreateGameResponse))]
[JsonSerializable(typeof(CreateGameRequest))]
[JsonSerializable(typeof(GameType))]
[JsonSerializable(typeof(SetMoveResponse))]
[JsonSerializable(typeof(SetMoveRequest))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}
