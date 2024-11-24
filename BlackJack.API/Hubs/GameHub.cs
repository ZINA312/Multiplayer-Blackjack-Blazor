using Microsoft.AspNetCore.SignalR;
using BlackJack.API.Models;
using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;
using Microsoft.EntityFrameworkCore;
using BlackJack.API.Services.GameService;
using BlackJack.API.Services.GameSessionService;

namespace BlackJack.API.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly IGameService _gameService;
        private readonly IGameSessionService _gameSessionService;
        public GameHub(IGameService service, IGameSessionService gameSessionService)
        {
            _gameService = service;
            _gameSessionService = gameSessionService;
        }

        public async Task CreateGame(string? gameName)
        {
            var gameSession = await _gameSessionService.GetGameSessionByNameAsync(gameName);
            if (gameSession.Successfull)
            {
                await Clients.Caller.ReceiveNotification("Game already exists. Please join the game.");
                return;
            }

            await Clients.Caller.ReceiveNotification("Creating the game...");
            var newGameSession = await _gameSessionService.CreateGameSessionAsync(gameName);
            if (!newGameSession.Successfull)
            {
                await Clients.Caller.ReceiveNotification("Error on creating the game!");
                return;
            }
        }

        public async Task JoinGame(UserConnection connection, string? playerName)
        {
            var gameSession = await _gameSessionService.GetGameSessionByIdAsync(connection.gameId);
            if (!gameSession.Successfull)
            {
                await Clients.Caller.ReceiveNotification("Game not found. Please create a new game.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, connection.gameId.ToString());
            await _gameSessionService.AddPlayerToGameSessionAsync(playerName, connection.gameId);
            await Clients.Group(connection.gameId.ToString())
                .ReceiveNotification($"Player {connection.userId} joined the game.");
        }

        public async Task LeaveGame(UserConnection connection)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.gameId.ToString());
            await _gameSessionService.DeletePlayerFromGameSessionAsync(connection.gameId, connection.userId);
            await Clients.Group(connection.gameId.ToString())
                .ReceiveNotification($"Player {connection.userId} left the game.");
        }

        public async Task StartGame(Guid gameId)
        {
            var responseData = await _gameSessionService.GetGameSessionByIdAsync(gameId);

            if (responseData.Successfull == false)
            {
                await Clients.Caller.ActionFailed("Game not found!");
                return;
            }
            var gameSession = responseData.Data;
            if (gameSession.Players.Count < 2)
            {
                await Clients.Caller.ActionFailed("Not enough players!");
                return;
            }
            
            await Clients.Group(gameId.ToString()).GameStarted("The game has started!", gameSession);
        }

        public async Task PlayerAction(Guid gameId, Guid playerId, string action)
        {
            var response = await _gameSessionService.GetGameSessionByIdAsync(gameId);
           
            if (response.Successfull == false)
            {
                await Clients.Caller.ActionFailed(response.ErrorMessage);
                return;
            }
            var gameSession = response.Data;
            var player = gameSession.Players.First(p => p.Id == playerId);
            if (player == null)
            {
                await Clients.Caller.ActionFailed("Player not found.");
                return;
            }
            ResponseData<GameSession> newResponse = new();
            switch (action.ToLower())
            {
                case "hit":
                    newResponse = await _gameService.PlayerHit(gameId, playerId);
                    if (newResponse.Successfull == false)
                    {
                        await Clients.Group(gameId.ToString()).ActionFailed("Action failed!");
                        break;
                    }
                    await Clients.Group(gameId.ToString()).PlayerHit(playerId, newResponse.Data);
                    break;

                case "stand":
                    newResponse = await _gameService.PlayerStand(gameId, playerId);
                    if (newResponse.Successfull == false)
                    {
                        await Clients.Group(gameId.ToString()).ActionFailed("Action failed!");
                        break;
                    }
                    await Clients.Group(gameId.ToString()).PlayerStood(playerId, newResponse.Data);
                    break;

                default:
                    await Clients.Caller.ActionFailed("Invalid action.");
                    break;
            }
        }
    }
}