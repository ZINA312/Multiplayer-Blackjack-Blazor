using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.SqlTypes;
using System.Net.Http.Json;
using BlackJack.Domain.Models;
using System.Text.Json;
using BlackJack.API.Models;

namespace BlackJack.BlazorWasm.Services;

public class MenuService:IMenuService
{
    private readonly HttpClient _httpClient;
    private Player player; 

    public MenuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }

    public async Task<bool> CreateGame(string gameName)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();

        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("CreateGame",  gameName );

            Console.WriteLine($"{connection}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<bool> JoinGame(string playerName, Guid gameId)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();
        player = new Player { Name = playerName, GameId = gameId };
        UserConnection userConnection = new UserConnection(player.Id, player.GameId);
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("JoinGame", userConnection, playerName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return true;
    }

    public async Task<ResponseData<ListModel<GameSession>>> GetGameSessionsList(int pageNo = 1, int pageSize = 3)
    {
        // Проверяем, задан ли BaseAddress для HttpClient
        if (_httpClient.BaseAddress == null)
        {
            Console.WriteLine("-----> BaseAddress для HttpClient не задан.");
            throw new InvalidOperationException("BaseAddress для HttpClient не задан.");
        }

        var url = $"api/games?pageNo={pageNo}&pageSize={pageSize}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<GameSession>>>(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        else
        {
            Console.WriteLine($"Ошибка при запросе: {response.StatusCode} {response.ReasonPhrase}");
            throw new HttpRequestException($"Не удалось получить список игр. Код ответа: {response.StatusCode}");
        }
    }

}
