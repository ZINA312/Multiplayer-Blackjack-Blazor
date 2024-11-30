using BlackJack.API.Data;
using BlackJack.API.Hubs;
using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlackJack.API.Services.GameSessionService
{
    public class GameSessionService : IGameSessionService
    {
        private readonly AppDbContext _context;
        private readonly int _maxPageSize = 10;

        public GameSessionService(AppDbContext dbContext, IHubContext<GameHub> hubContext)
        {
            _context = dbContext;
        }

        public async Task<ResponseData<ListModel<GameSession>>> GetGameSessionsListAsync(int pageNo = 1, int pageSize = 10)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            var dataList = new ListModel<GameSession>();
            var query = _context.game.AsQueryable();
            var totalCount = await query.CountAsync();

            dataList.Items = [.. query.OrderBy(d => d.GameId)
                                    .Skip((pageNo - 1) * pageSize)
                                    .Take(pageSize)];
            dataList.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (pageNo > dataList.TotalPages)
            {
                return ResponseData<ListModel<GameSession>>.Error("No such page");
            }  
            dataList.CurrentPage = pageNo;
            return ResponseData<ListModel<GameSession>>.Success(dataList);
        }

        public async Task<ResponseData<GameSession>> GetGameSessionByIdAsync(Guid id)
        {
            var response = new ResponseData<GameSession>();
            var gameSession = await _context.game.FindAsync(id);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> GetGameSessionByNameAsync(string gameName)
        {
            var response = new ResponseData<GameSession>();
            var gameSession = await _context.game.FirstAsync(g => g.Name == gameName);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> CreateGameSessionAsync(string? name)
        {
            GameSession gameSession = new();
            if (name == null)
            {
                gameSession.Name = gameSession.GameId.ToString();
            }
            else
            {
                gameSession.Name = name;
            }
            await _context.game.AddAsync(gameSession);
            await _context.SaveChangesAsync();
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> AddPlayerToGameSessionAsync(string name, Guid gameId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }

            var player = new Player { Name = name, GameId = gameId, GameSession = gameSession };
            gameSession.Players.Add(player);
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();

            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<bool>> DeletePlayerFromGameSessionAsync(Guid gameId, Guid playerId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<bool>.Error("Game session not found!");
            }

            var player = gameSession.Players.First(p => p.Id == playerId);
            if (player == null)
            {
                return ResponseData<bool>.Error("Player not found!");
            }
            gameSession.Players.Remove(player);
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();
            return ResponseData<bool>.Success(true);
        }

        public async Task<ResponseData<bool>> DeleteGameSessionAsync(Guid gameId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession != null)
            {
                _context.game.Remove(gameSession);
                await _context.SaveChangesAsync();
                return ResponseData<bool>.Success(true);
            }
            return ResponseData<bool>.Error("Game session not found!");
        }

        
    }
}