using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlackJack.Domain.Entities
{
    public class Dealer : Person
    {
        [JsonPropertyName("gameId")]
        public Guid? GameId { get; set; }

        [JsonIgnore]
        public GameSession GameSession { get; set; }

        [JsonPropertyName("cardDeck")]
        public Deck CardDeck { get; set; }

        public Dealer() { }


        public static Dealer Create(GameSession session)
        {
            var dealer = new Dealer
            {
                GameSession = session,
                GameId = session.GameId
            };
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