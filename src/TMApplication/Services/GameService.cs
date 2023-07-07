﻿using TMApplication.Providers;
using TMApplication.ViewModels;

namespace TMApplication.Services;

public class GameService
{
    private readonly IDataProvider _dataProvider;

    public GameService(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async Task<LeagueGameSummaryViewModel> GetGameSummaryVm(int? gameId, CancellationToken cancellationToken = default)
    {
        var game = gameId == null ?
            null : 
            await _dataProvider.GetGame(gameId.Value, cancellationToken);
        if (game == null)
            return new LeagueGameSummaryViewModel(gameId, null, 0, 0, false, false, null, null);
        
        return new LeagueGameSummaryViewModel(gameId, game.Name, game.Progress, game.Turn, game.IsFinished,
            game.IsStalling, game.IsFinished ? game.Houses.First().Player : null, game.GeneratedTime);
    }

    public async Task<GameViewModel?> GetGameVm(int gameId, CancellationToken cancellationToken = default)
    {
        var game = await _dataProvider.GetGame(gameId, cancellationToken);
        if (game == null)
            return null;

        return new GameViewModel(game.Id, game.Name, game.IsFinished, game.IsStalling, game.Turn, game.Houses, game.GeneratedTime);
    }
}