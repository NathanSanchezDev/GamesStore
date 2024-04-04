using GamesStore.Api.Dtos;

namespace GamesStore.Api.EndPoints;

public static class GamesEndpoints
{
    private const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> Games =
    [
        new GameDto(
            1,
            "Street Fighter II",
            "Fighting",
            19.99M,
            new DateOnly(1992, 7, 15)
        ),
        new GameDto(
            2,
            "Final Fantasy XIV",
            "Role Playing",
            59.99M,
            new DateOnly(2010, 9, 30)
        ),
        new GameDto(
            3,
            "FIFA 23",
            "Sports",
            69.99M,
            new DateOnly(2022, 9, 27)
        )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();
        
        // GET /games
        group.MapGet("/", () => Games);

        // GET /games/1
        group.MapGet("/{id:int}", (int id) =>
            {
                var game = Games.Find(game => game.Id == id);

                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(GetGameEndpointName);

// POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                Games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);
    
            Games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id:int}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = Games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
    
            Games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

//Delete /games/1
        group.MapDelete("/{id:int}", (int id) =>
        {
            Games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });     
        
        return group;
    }
    
}