using System;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games = 
    [
        new(){
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateOnly(1992,7,15)
        },

        new(){ 
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplayting",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010,9,30)
        },

        new(){
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sport",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022,9,27)
        }
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => games.ToArray();

    public void AddGame(GameDetails game){
        
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
        var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));

        var gameSummary = new GameSummary
        {
            Id = games.Count + 1,
            Name = game.Name,
            Genre = genre.Name,
            ReleaseDate = game.ReleaseDate

        };
        games.Add(gameSummary);
    }
}
