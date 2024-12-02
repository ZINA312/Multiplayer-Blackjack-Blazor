namespace BlackJack.BlazorWasm.Services.InGameService;

public interface IInGameService
{
    public Task<bool> LeaveGame();
    public Task<bool> StartGame();
    public Task<bool> PlayerAction(string action);
}
