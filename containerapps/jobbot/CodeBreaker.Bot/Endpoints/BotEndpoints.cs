using CodeBreaker.Bot.Api;
using CodeBreaker.Bot.Exceptions;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreaker.Bot.Endpoints;

public static class BotEndpoints
{
    public static void MapBotEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/bots", Results<Accepted<Guid>, BadRequest<string>> (
            [FromServices] CodeBreakerTimer timer,
            [FromQuery] int? delay,
            [FromQuery] int? count,
            [FromQuery] int? thinkTime) =>
        {
            Guid id;

            try
            {
                id = timer.Start(delay ?? 60, count ?? 3, thinkTime ?? 3);
            }
            catch (ArgumentOutOfRangeException)
            {
                return TypedResults.BadRequest("Invalid options");
            }

            return TypedResults.Accepted($"/games/{id}", id);
        })
        .WithName("CreateBot")
        .WithSummary("Starts a bot playing one or more games")
        .WithOpenApi(x =>
        {
            x.Parameters[0].Description = "The delay between the games (seconds). If not specified, default values are used.";
            x.Parameters[1].Description = "The number of games to play. If not specified, default values are used.";
            x.Parameters[2].Description = "The think time between game moves (seconds). If not specified, default values are used.";
            return x;
        });

        routes.MapGet("/bots/{id}", Results<Ok<StatusResponse>, NotFound, BadRequest<string>> (Guid id) =>
        {
            StatusResponse result;

            try
            {
                result = CodeBreakerTimer.Status(id);
            }
            catch (ArgumentException)
            {
                return TypedResults.BadRequest("Invalid id");
            }
            catch (BotNotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        })
        .WithName("GetBot")
        .WithSummary("Gets the status of a bot")
        .WithOpenApi(x =>
        {
            x.Parameters[0].Description = "The id of the bot";
            return x;
        });

        routes.MapDelete("/bots/{id}", Results<BadRequest<string>, NotFound, NoContent> (Guid id) =>
        {
            try
            {
                CodeBreakerTimer.Stop(id);
            }
            catch (ArgumentException)
            {
                return TypedResults.BadRequest("Invalid id");
            }
            catch (BotNotFoundException)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.NoContent();
        })
        .WithName("StopBot")
        .WithSummary("Stops the bot with the given id")
        .WithOpenApi(x =>
        {
            x.Parameters[0].Description = "The id of the bot";
            return x;
        });

    }
}
