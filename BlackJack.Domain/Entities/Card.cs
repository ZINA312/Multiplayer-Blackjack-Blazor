using System;
using System.Text.Json.Serialization;

namespace BlackJack.Domain.Entities
{
    public enum CardSuit
    {
        [JsonPropertyName("hearts")]
        Hearts,

        [JsonPropertyName("clubs")]
        Clubs,

        [JsonPropertyName("diamonds")]
        Diamonds,

        [JsonPropertyName("spades")]
        Spades
    }

    public enum CardValue
    {
        [JsonPropertyName("two")]
        Two = 2,

        [JsonPropertyName("three")]
        Three = 3,

        [JsonPropertyName("four")]
        Four = 4,

        [JsonPropertyName("five")]
        Five = 5,

        [JsonPropertyName("six")]
        Six = 6,

        [JsonPropertyName("seven")]
        Seven = 7,

        [JsonPropertyName("eight")]
        Eight = 8,

        [JsonPropertyName("nine")]
        Nine = 9,

        [JsonPropertyName("ten")]
        Ten = 10,

        [JsonPropertyName("jack")]
        Jack = 11,

        [JsonPropertyName("queen")]
        Queen = 12,

        [JsonPropertyName("king")]
        King = 13,

        [JsonPropertyName("ace")]
        Ace = 1
    }

    public class Card
    {
        [JsonPropertyName("suit")]
        public CardSuit Suit { get; set; }

        [JsonPropertyName("value")]
        public CardValue Value { get; set; }

        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; } = true;

        [JsonPropertyName("imageName")]
        public required string ImageName { get; set; }

        [JsonPropertyName("score")]
        public int Score
        {
            get
            {
                if (Value == CardValue.King || Value == CardValue.Queen)
                {
                    return 10;
                }
                if (Value == CardValue.Ace)
                {
                    return 11;
                }
                return (int)Value;
            }
            set
            {
            }
        }

        [JsonPropertyName("isTenCard")]
        public bool IsTenCard
        {
            get
            {
                return Value == CardValue.Ten
                       || Value == CardValue.Jack
                       || Value == CardValue.Queen
                       || Value == CardValue.King;
            }
            set
            {
            }
        }

        public Card() { }
    }
}