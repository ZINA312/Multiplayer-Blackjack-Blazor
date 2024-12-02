namespace BlackJack.BlazorWasm.Services.InGameService;

public interface IInGameService
{
    public Task<bool> LeaveGame(Guid userId, Guid gameId);
    public Task<bool> StartGame(Guid gameId);
    public Task<bool> PlayerAction(Guid userId, Guid gameId, string action);
}
