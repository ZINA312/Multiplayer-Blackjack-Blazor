using BlackJack.Domain.Extensions;

namespace BlackJack.Domain.Entities
{
    public class Deck
    {
        public Deck() { }
        public Guid Id { get; set; } = new();
        public Guid GameId { get; set; }
        public required GameSession GameSession { get; set; } 
        protected Stack<Card> Cards { get; set; } = new Stack<Card>();
        public static Deck Create(GameSession session)
        {
            var deck = new Deck
            {
                GameSession = session,
                GameId = session.GameId
            };

            List<Card> cards = [];

            // Creating cards
            foreach (CardSuit suit in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in (CardValue[])Enum.GetValues(typeof(CardValue)))
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
        public int Count
        {
            get
            {
                return Cards.Count;
            }
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
