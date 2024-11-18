
namespace BlackJack.Domain.Entities
{
    public class Dealer : Person
    {
        public Deck CardDeck { get; set; } = new Deck();
        public Card Deal()
        {
            return CardDeck.Draw();
        }
        public async Task DealToSelf()
        {
            await AddCard(Deal());
        }

        public async Task DealToPlayer(Player player)
        {
            await player.AddCard(Deal());
        }
    }
}
