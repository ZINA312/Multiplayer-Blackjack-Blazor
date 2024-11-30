using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;

namespace BlackJack.API.Services.GameSessionService
{
    public interface IGameSessionService
    {
        public Task<ResponseData<ListModel<GameSession>>> GetGameSessionsListAsync(int pageNo = 1, int pageSize = 10);
        public Task<ResponseData<GameSession>> GetGameSessionByIdAsync(Guid id);
        public Task<ResponseData<GameSession>> GetGameSessionByNameAsync(string gameName);
        public Task<ResponseData<GameSession>> CreateGameSessionAsync(string? name);
        public Task<ResponseData<GameSession>> AddPlayerToGameSessionAsync(string name, Guid gameId);
        public Task<ResponseData<bool>> DeletePlayerFromGameSessionAsync(Guid gameId, Guid playerId);
        public Task<ResponseData<bool>> DeleteGameSessionAsync(Guid gameId);
    }
}