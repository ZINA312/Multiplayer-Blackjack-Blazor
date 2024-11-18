using BlackJack.Domain.Extensions;

namespace BlackJack.Domain.Entities
{
    public class Deck
    {
        protected Stack<Card> Cards { get; set; } = new Stack<Card>();
        public Deck()
        {
            List<Card> cards = [];
            //creating cards
            foreach (CardSuit suit
                     in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value
                         in (CardValue[])Enum.GetValues(typeof(CardValue)))
                {
                    Card newCard = new()
                    {
                        Suit = suit,
                        Value = value,
                        ImageName = "card" + suit.GetDisplayName()
                                    + value.GetDisplayName()
                    };

                    cards.Add(newCard);
                }
            }
            //shuffle 
            var array = cards.ToArray();
            Random rnd = new();

            for (int n = array.Length - 1; n > 0; --n)
            {
                int k = rnd.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }

            for (int n = 0; n < array.Count(); n++)
            {
                Cards.Push(array[n]);
            }
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
