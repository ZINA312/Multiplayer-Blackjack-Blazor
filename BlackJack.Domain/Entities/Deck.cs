using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using BlackJack.Domain.Extensions;

namespace BlackJack.Domain.Entities
{
    public class Deck
    {
        public Deck() { }

        [JsonPropertyName("id")]
        public Guid Id { get; set; } = new();

        [JsonPropertyName("gameId")]
        public Guid? GameId { get; set; }

        [JsonIgnore]
        public GameSession GameSession { get; set; }

        [JsonIgnore]
        public int Count { get { return Cards.Count; }
        }

        protected Stack<Card> Cards { get; set; } = new Stack<Card>();

        public static Deck Create(GameSession session)
        {
            var deck = new Deck
            {
                GameSession = session,
                GameId = session.GameId
            };

            List<Card> cards = new List<Card>();

            // Creating cards
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    Card newCard = new()
                    {
                        Suit = suit,
                        Value = value,
                        ImageName = "card" + suit.GetDisplayName() + value.GetDisplayName()
                    };

                    cards.Add(newCard);
                }
            }

            // Shuffle
            var array = cards.ToArray();
            Random rnd = new();

            for (int n = array.Length - 1; n > 0; --n)
            {
                int k = rnd.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            foreach (var card in array)
            {
                deck.Cards.Push(card);
            }

            return deck;
        }

        public void Add(Card card)
        {
            Cards.Push(card);
        }

        public Card Draw()
        {
            return Cards.Pop();
        }
    }
}