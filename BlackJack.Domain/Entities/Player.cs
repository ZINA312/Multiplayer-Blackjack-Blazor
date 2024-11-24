
namespace BlackJack.Domain.Entities
{
    public class Player : Person
    {
        public Guid GameId { get; set; }
        public required string Name { get; set; }
        public required GameSession GameSession { get; set; }    
    }
}
