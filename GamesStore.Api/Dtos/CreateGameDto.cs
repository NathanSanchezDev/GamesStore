using System.ComponentModel.DataAnnotations;

namespace GamesStore.Api.Dtos;

public abstract record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)]string Genre,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate
    );