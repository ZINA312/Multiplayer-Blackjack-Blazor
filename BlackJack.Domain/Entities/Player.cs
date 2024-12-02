using System;
using System.Text.Json.Serialization;

namespace BlackJack.Domain.Entities
{
    public class Player : Person
    {
        [JsonPropertyName("gameId")]
        public Guid GameId { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonIgnore]
        public GameSession GameSession { get; set; }

        public Player() { }
    }
}