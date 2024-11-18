
namespace BlackJack.API.Models
{
    public interface IGameClient
    {
        public Task RecieveNotification(string userName, string message);
    }
}
