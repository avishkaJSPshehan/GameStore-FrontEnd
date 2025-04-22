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

    public GameDetails? GetGame(int id)
    {
        var gameSummary = games.SingleOrDefault(g => g.Id == id);
        if (gameSummary is null)
        {
            return null;
        }

        var genre = genres.SingleOrDefault(g => g.Name == gameSummary.Genre);

        return new GameDetails
        {
            Id = gameSummary.Id,
            Name = gameSummary.Name,
            GenreId = genre?.Id.ToString() ?? string.Empty,
            Price = gameSummary.Price,
            ReleaseDate = gameSummary.ReleaseDate
        };
    }


    public void AddGame(GameDetails game)
    {
        Genre genre = GetGenreById(game.GenreId);

        var gameSummary = new GameSummary
        {
            Id = games.Count + 1,
            Name = game.Name,
            Genre = genre.Name,
            ReleaseDate = game.ReleaseDate

        };
        games.Add(gameSummary);
    }

    public void UpdateGame(GameDetails updatedGame){

        var genre = GetGenreById(updatedGame.GenreId);
        GameSummary existingGame = GetGameSumaryById(updatedGame.Id);

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = genre.Name;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    private GameSummary GetGameSumaryById(int id){
        GameSummary? game = games.Find(game => game.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }
    private Genre GetGenreById(string? id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return genres.Single(genre => genre.Id == int.Parse(id));
    }
}
