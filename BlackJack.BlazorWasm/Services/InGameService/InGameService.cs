using Microsoft.AspNetCore.SignalR.Client;

namespace BlackJack.BlazorWasm.Services.InGameService;

public class InGameService: IInGameService
{
    public async Task<bool> LeaveGame()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();

        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("LeaveGame");

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<bool> StartGame()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();

        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("StartGame");

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<bool> PlayerAction(string action)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();

        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("PlayerAction", action);

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }
}
