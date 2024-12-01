using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System.Data.SqlTypes;
using System.Text.Json;

namespace BlackJack.BlazorWasm.Services;

public class MenuService:IMenuService
{
    private readonly HttpClient _httpClient;

    public MenuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateGame(string gameName )
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

    public async Task<bool> JoinGame(string playerName)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7052/game")
            .WithAutomaticReconnect()
            .Build();
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("JoinGame", playerName);
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

            return JsonSerializer.Deserialize<ResponseData<ListModel<GameSession>>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр свойств
            });
        }
        else
        {
            Console.WriteLine($"Ошибка при запросе: {response.StatusCode} {response.ReasonPhrase}");
            throw new HttpRequestException($"Не удалось получить список игр. Код ответа: {response.StatusCode}");
        }
    }

}
