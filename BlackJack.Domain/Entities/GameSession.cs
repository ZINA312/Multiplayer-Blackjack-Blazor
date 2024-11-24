
namespace BlackJack.Domain.Entities
{
    public class GameSession
    {
        public Guid GameId { get; set; } = new();
        public string Name { get; set; }
        public int PlayersCount { get; set; } = 0;
        public List<Player> Players { get; set; } = [];
        public int CurrentPlayerIndex { get; set; } = 0;
        public Dealer Dealer { get; set; }
        public Deck Deck { get; set; }

        public GameSession() 
        {
            Dealer = Dealer.Create(this);
            Deck = Deck.Create(this);
            Dealer.CardDeck = Deck;
        }

        public void NextPlayer()
        {
            if (PlayersCount == 0)
                return;
            do
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersCount;
            }
            while (Players[CurrentPlayerIndex].IsStand);
        }

        public bool AllPlayersStand()
        {
            foreach (var player in Players)
            {
                if (!player.IsStand) return false;
            }
            return true;
        }
    }
}
