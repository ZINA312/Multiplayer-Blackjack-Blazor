using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;

namespace BlackJack.BlazorWasm.Services;

public interface IMenuService
{
    public Task<bool> CreateGame(string gameName);
    public Task<bool> JoinGame(string playerName);
    public Task<ResponseData<ListModel<GameSession>>> GetGameSessionsList(int pageNo = 1, int pageSize = 3);

}
