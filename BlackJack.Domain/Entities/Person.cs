
namespace BlackJack.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; } = new();
        public List<Card> Cards { get; set; } = [];
        public int Score
        {
            get
            {
                return ScoreCalculation();
            }
        }
        public int VisibleScore
        {
            get
            {
                return ScoreCalculation(true);
            }
        }
        public bool IsStand { get; set; } = false;
        public bool HasNaturalBlackjack =>
        Cards.Count == 2
        && Score == 21
        && Cards.Any(x => x.Value == CardValue.Ace)
        && Cards.Any(x => x.IsTenCard);
        public bool IsBusted => Score > 21;
        private int ScoreCalculation(bool onlyVisible = false)
        {
            var cards = Cards;

            //1. Filter the Cards to only include the visible cards
            //   if that parameter is set to true.
            if (onlyVisible)
            {
                cards = Cards.Where(x => x.IsVisible).ToList();
            }

            //2. If the sum total of all cards is <= 21, return that value
            var totalScore = cards.Sum(x => x.Score);
            if (totalScore <= 21) return totalScore;

            //3. If there are no Aces and the sum total is > 21, 
            //   the person has bust
            bool hasAces = cards.Any(x => x.Value == CardValue.Ace);
            if (!hasAces && totalScore > 21) return totalScore;

            //By this point, the sum will be greater than 21 
            //  if all the Aces are worth 11
            //So, make each Ace worth 1, until the sum is <= 21

            var acesCount = cards.Where(x => x.Value == CardValue.Ace).Count();
            var acesScore = cards.Sum(x => x.Score);

            //5. While there are Aces left...
            while (acesCount > 0)
            {
                //6. Make an Ace worth 1 point
                acesCount -= 1;
                acesScore -= 10;
                //7. If the score is now less than or equal to 21, 
                //   return the score.
                if (acesScore <= 21) return acesScore;
            } //8

            //9. If the score never gets returned, the person has bust
            return cards.Sum(x => x.Score);
        }
        public string ScoreDisplay
        {
            get
            {
                if (HasNaturalBlackjack && Cards.All(x => x.IsVisible))
                    return "Blackjack!";

                //We use Visible Score because this display
                //should only show the Dealer's visible score,
                //and the Player's visible score and true score
                //will always be the same value.
                var score = VisibleScore;

                if (score > 21)
                    return "Busted!";
                else return score.ToString();
            }
        }
        public async Task AddCard(Card card)
        {
            Cards.Add(card);
            await Task.Delay(300);
        }
        public void ClearHand()
        {
            Cards.Clear();
        }
    }
}
