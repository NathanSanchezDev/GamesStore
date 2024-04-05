using GamesStore.Api.Data;
using GamesStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

const string connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();