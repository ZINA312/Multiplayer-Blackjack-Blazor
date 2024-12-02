using BlackJack.API.Models;
using BlackJack.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlackJack.BlazorWasm.Services.InGameService;

public class InGameService: IInGameService
{
    public async Task<bool> LeaveGame(Guid userId , Guid gameId)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();
        UserConnection userConnection = new UserConnection(userId, gameId);
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("LeaveGame" , userConnection);

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<bool> StartGame(Guid gameId)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();

        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("StartGame" , gameId);

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<bool> PlayerAction(Guid userId, Guid gameId , string action)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();
        UserConnection userConnection = new UserConnection(userId, gameId);
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("PlayerAction", userConnection, action);

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }
}
