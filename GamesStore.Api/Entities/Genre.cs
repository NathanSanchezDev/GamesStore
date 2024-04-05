namespace GamesStore.Api.Entities;
using System.ComponentModel.DataAnnotations; // Add this line

public class Genre
{
    [Key] // Add this line above the Id property
    public int Id { get; set; } 
    
    public required string Name { get; set; }
}