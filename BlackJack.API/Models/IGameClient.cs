
using BlackJack.Domain.Entities;

namespace BlackJack.API.Models
{
    public interface IGameClient
    {
        Task ReceiveNotification(string message);
        Task GameStarted(string message, GameSession game);
        Task ActionFailed(string errorMessage);
        Task PlayerHit(Guid playerId, GameSession game);
        Task PlayerStood(Guid playerId, GameSession game);
    }
}
