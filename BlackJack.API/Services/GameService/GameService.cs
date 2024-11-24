using BlackJack.API.Data;
using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;

namespace BlackJack.API.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<ResponseData<GameSession>> StartGame(Guid gameId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            await gameSession.Dealer.DealToSelf(false);
            await gameSession.Dealer.DealToSelf(true);
            if(gameSession.Dealer.Score == 21)
            {
                gameSession.Dealer.Cards.ForEach(c => c.IsVisible = true);
            }
            for (var i = 0; i < gameSession.PlayersCount; i++)
            {
                await gameSession.Dealer.DealToPlayer(gameSession.Players[i]);
                await gameSession.Dealer.DealToPlayer(gameSession.Players[i]);
            }
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();
            return ResponseData<GameSession>.Success(gameSession);
        }
        public async Task<ResponseData<GameSession>> DealerHit(Guid gameId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            await gameSession.Dealer.DealToSelf(true);
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> PlayerHit(Guid gameId, Guid playerId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            if (gameSession.Players[gameSession.CurrentPlayerIndex].Id != playerId)
            {
                return ResponseData<GameSession>.Error("It's not your turn!");
            }
            await gameSession.Dealer.DealToPlayer(gameSession.Players.First(p => p.Id == playerId));
            gameSession.NextPlayer();
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> PlayerStand(Guid gameId, Guid playerId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            if (gameSession.Players[gameSession.CurrentPlayerIndex].Id != playerId)
            {
                return ResponseData<GameSession>.Error("It's not your turn!");
            }
            gameSession.Players.First(p => p.Id == playerId).IsStand = true;
            gameSession.NextPlayer();
            _context.game.Update(gameSession);
            await _context.SaveChangesAsync();
            if (gameSession.AllPlayersStand())
            {
                return await StopGame(gameId);
            }
            
            return ResponseData<GameSession>.Success(gameSession);
        }

        public async Task<ResponseData<GameSession>> StopGame(Guid gameId)
        {
            var gameSession = await _context.game.FindAsync(gameId);
            if (gameSession == null)
            {
                return ResponseData<GameSession>.Error("Game session not found!");
            }
            _context.game.Remove(gameSession);
            await _context.SaveChangesAsync();
            gameSession.Dealer.Cards.ForEach(c => c.IsVisible = true);
            if (gameSession.Dealer.Score < 17)
            {
                await gameSession.Dealer.DealToSelf(true);
            }
            return ResponseData<GameSession>.Success(gameSession);
        }
    }
}
