
namespace BlackJack.Domain.Entities
{
    public enum CardSuit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum CardValue
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 1,
    }

    public class Card
    {
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public bool IsVisible { get; set; } = true;
        public required string ImageName { get; set; }
        public int Score
        {
            get
            {
                if (Value == CardValue.King
                    || Value == CardValue.Queen)
                {
                    return 10;
                }
                if (Value == CardValue.Ace)
                {
                    return 11;
                }
                else
                {
                    return (int)Value;
                }
            }

        }
        public bool IsTenCard
        {
            get
            {
                return Value == CardValue.Ten
                        || Value == CardValue.Jack
                        || Value == CardValue.Queen
                        || Value == CardValue.King;
            }
        }

    }
}
