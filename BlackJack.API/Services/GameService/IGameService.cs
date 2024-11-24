using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;

namespace BlackJack.API.Services.GameService
{
    public interface IGameService
    {
        public Task<ResponseData<GameSession>> StartGame(Guid gameId);
        public Task<ResponseData<GameSession>> StopGame(Guid gameId);
        public Task<ResponseData<GameSession>> PlayerHit(Guid gameId, Guid playerId);
        public Task<ResponseData<GameSession>> PlayerStand(Guid gameId, Guid playerId);
        public Task<ResponseData<GameSession>> DealerHit(Guid gameId);
    
    }
}
