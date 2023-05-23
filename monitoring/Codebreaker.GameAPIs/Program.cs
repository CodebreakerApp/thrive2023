using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

using Azure.Core.Diagnostics;
using Azure.Identity;

using Codebreaker.APIs.Extensions;
using Codebreaker.GameAPIs.Data;
using Codebreaker.GameAPIs.Data.InMemory;
using Codebreaker.GameAPIs.Utilities;

using CodeBreaker.APIs.Options;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;

[assembly: InternalsVisibleTo("Codbreaker.APIs.Tests")]

// ASP.NET Core registers the ASP.NET Core ActivitySource as singleton with the DI container.
// To keep this ASP.NET Core used instance active, and activities are only started from the API endpoints, create
// one here, and pass it to the Map method
ActivitySource activitySource = new("CNinnovation.CodeBreaker.API");

#if DEBUG
using var listener =
    AzureEventSourceListener.CreateConsoleLogger(EventLevel.Informational);

DefaultAzureCredentialOptions options = new()
{
    Diagnostics =
    {
        LoggedHeaderNames = { "x-ms-request-id" },
        LoggedQueryParameters = { "api-version " },
        IsAccountIdentifierLoggingEnabled = true,
        IsDistributedTracingEnabled = true,
        IsLoggingContentEnabled = true,
        IsLoggingEnabled = true,
        IsTelemetryEnabled = true,
    }
};

AzureCliCredentialOptions options2 = new()
{
    Diagnostics =
    {
        LoggedHeaderNames = { "x-ms-request-id" },
        LoggedQueryParameters = { "api-version " },
        IsAccountIdentifierLoggingEnabled = true,
        IsDistributedTracingEnabled = true,
        IsLoggingContentEnabled = true,
        IsLoggingEnabled = true,
        IsTelemetryEnabled = true,
    }
};
AzureCliCredential azureCredential = new(options2);
//DefaultAzureCredential azureCredential = new(options);

#else
DefaultAzureCredential azureCredential = new();
#endif

var builder = WebApplication.CreateBuilder(args);

try
{
    // AppConfiguration
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        try
        {
            string endpoint = builder.Configuration["AzureAppConfigurationEndpoint"] ?? throw new InvalidOperationException("AzureAppConfigurationEndpoint");
            options.Connect(new Uri(endpoint), azureCredential)
                .Select("ApiService*", LabelFilter.Null)
                .Select("ApiService*", builder.Environment.EnvironmentName)
                .ConfigureKeyVault(vault => vault.SetCredential(azureCredential));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    });
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

builder.Services.AddAzureClients(options =>
{
    //Uri queueUri = new(builder.Configuration["ApiService:Storage:Queue:ServiceUri"] ?? throw new InvalidOperationException("ApiService:Storage:Queue:ServiceUri configuration is not available"));
    //options.AddQueueServiceClient(queueUri);
    // Add EventHubClient here
    options.UseCredential(azureCredential);
});

builder.Logging.AddOpenTelemetryLogging();

builder.Services.AddAzureAppConfiguration();

builder.Services.AddOpenTelemetryTracing();
builder.Services.AddOpenTelemetryMetrics();

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.Configure<ApiServiceOptions>(builder.Configuration.GetSection("ApiService"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApiServiceOptions>>().Value);

//#if NET8_0_OR_GREATER
//// JSON Serialization - do not enable this before .NET 8
//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.SerializerOptions.AddContext<GamesJsonSerializerContext>();
//});
//#endif

// ApplicationInsights
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, ApplicationInsightsTelemetryInitializer>();

// Swagger/EndpointDocumentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddSingleton<ICodebreakerRepository, MemoryRepository>();

//builder.Services.AddDbContext<ICodebreakerRepository, CodebreakerCosmosContext>(options =>
//{
//    string accountEndpoint = builder.Configuration["ApiService:Cosmos:AccountEndpoint"]
//        ?? throw new InvalidOperationException("ApiService:Cosmos:AccountEndpoint configuration is not available");
//    string databaseName = builder.Configuration["ApiService:Cosmos:DatabaseName"]
//        ?? throw new InvalidOperationException("ApiService:Cosmos:DatabaseName configuration is not availabile");
//    // options.UseCosmos(accountEndpoint, azureCredential, databaseName);

//    string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
//    options.UseCosmos(connectionString, databaseName);
//});

// Cache
builder.Services.AddMemoryCache();

// Application Services

builder.Services.AddScoped<IGamesService, GamesService>();

// CORS
const string AllowCodeBreakerOrigins = "_allowCodeBreakerOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCodeBreakerOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddRequestDecompression();

var app = builder.Build();

app.UseCors(AllowCodeBreakerOrigins);
app.UseRequestDecompression();

app.UseSwagger();
app.UseSwaggerUI();

// -------------------------
// Endpoints
// -------------------------

app.MapGameEndpoints(app.Logger, activitySource);

app.Run();
