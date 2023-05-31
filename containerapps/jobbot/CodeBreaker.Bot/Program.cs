using CodeBreaker.APIs;
using CodeBreaker.Bot.Endpoints;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MMBot.Tests")]

var builder = WebApplication.CreateBuilder(args);

// ApplicationInsights
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, ApplicationInsightsTelemetryInitializer>();

WebApplication? app = null;

// Swagger & EndpointDocumentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClient & Application Services
builder.Services.AddHttpClient<CodeBreakerGameRunner>(options =>
{
    string codebreakeruri = builder.Configuration["ApiBase"]
        ?? throw new InvalidOperationException("ApiBase configuration not available"); ;

    Uri apiUri = new(codebreakeruri);

    app?.Logger.UsingUri(apiUri.ToString());
    options.BaseAddress = apiUri;
});
builder.Services.AddScoped<CodeBreakerTimer>();

app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapBotEndpoints();

app.Run();
