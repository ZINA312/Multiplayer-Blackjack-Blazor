
namespace BlackJack.Domain.Entities
{
    public class Dealer : Person
    {
        public Guid GameId { get; set; }
        public GameSession GameSession { get; set; }
        public Deck CardDeck { get; set; }
        public static Dealer Create(GameSession session)
        {
            var dealer = new Dealer();
            dealer.GameSession = session;
            dealer.GameId = session.GameId;
            return dealer;
        }
        public Card Deal()
        {
            return CardDeck.Draw();
        }
        public async Task DealToSelf(bool isVisible)
        {
            Card card = Deal();
            card.IsVisible = isVisible;
            await AddCard(card);
        }

        public async Task DealToPlayer(Player player)
        {
            await player.AddCard(Deal());
        }
    }
}
