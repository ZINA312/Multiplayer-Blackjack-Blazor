using Microsoft.AspNetCore.SignalR;
using BlackJack.API.Models;

namespace BlackJack.API.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        public async Task JoinGame(UserConnection connection)
        {
            //TODO Getting username from db
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.gameId.ToString());
            await Clients
                .Group(connection.gameId.ToString())
                .RecieveNotification("Admin", $"Player {connection.userId} connected!");
        }
    }
}
