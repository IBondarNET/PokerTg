using PokerLib.Enums;

namespace PokerLib;

public class PokerHand
{
    public static void Run()
    {
        // GetValueCardsInCombination(2, new Card[]
        // {
            // new(Suits.Spades, Rank.Ace),
            // new(Suits.Clubs, Rank.King),
            // new(Suits.Spades, Rank.King),
            // new(Suits.Clubs, Rank.Eight),
            // new(Suits.Spades, Rank.Eight),
            // new(Suits.Spades, Rank.Ten),
            // new(Suits.Hearts, Rank.Queen),
            // new(Suits.Spades, Rank.Seven),
        // });

    }

    public static int GetValueCardsInCombination(int combinationValue, Card[] cards)
    {
        List<Card> combinationCards;
        switch (combinationValue)
        {
            case 0:
            {
                combinationCards = cards.OrderByDescending(c => c.Rank).Take(5).ToList();
                return combinationCards.Select(c => c.Rank).Select(r => (int)r).Sum();
            }
            case 1:
            {
                var pair = cards.GroupBy(c => c.Rank).First(g => g.Count() == 2).ToArray();
                var threeCards = cards.Except(pair).OrderByDescending(c => c.Rank).Take(3).ToArray();
                return pair.Select(c => c.Rank).Select(r => (int)r).Aggregate(1, (i, iNext) => i * iNext)
                       + threeCards.Select(c => c.Rank).Select(r => (int)r).Sum();
            }
            case 2:
            {
                var twoPair = cards.GroupBy(c => c.Rank)
                    .Where(g => g.Count() == 2)
                    .OrderByDescending(r => r.Key)
                    .Take(2)
                    .ToArray();
                var twoPairValue = twoPair.Sum(pair => (int)pair.Key * (int)pair.Key);
                var listRank = twoPair.Select(a => a.Key).ToList();
                var cardsWithoutTwoPair = cards.Where(a => !listRank.Contains(a.Rank)).ToArray();

                return twoPairValue + (int)cardsWithoutTwoPair.MaxBy(a => a.Rank).Rank;
            }
            case 3:
            {
                var threeCards = cards.GroupBy(c => c.Rank).First(g => g.Count() == 3).ToArray();
                var cardsWithoutThreeCards = cards.Except(threeCards).OrderByDescending(c=>c.Rank).Take(2).ToArray();
                return (int)threeCards[0].Rank * (int)threeCards[0].Rank * (int)threeCards[0].Rank +
                       cardsWithoutThreeCards.Sum(r => (int)r.Rank);
            }
            case 4:
            {
                HasStraight(cards, out var combination);
                if (combination.Any(c=>c.Rank is Rank.Ace) && combination.Any(c=>c.Rank is Rank.Two))
                {
                    return 15;
                }
                
                return combination.Select(c => (int)c.Rank).Sum();
            }
            case 5:
                return 5;
            case 6:
                return 6;
            case 7:
                return 7;
            case 8:
                return 8;
        }

        return 0;
    }

    public static int GetCombinationValue(Card[] cards)
    {
        if (HasRoyalFlesh(cards))
        {
            return 9;
        }

        if (HasStraightFlesh(cards))
        {
            return 8;
        }

        if (HasFourOfAKind(cards))
        {
            return 7;
        }

        if (HasFullHouse(cards))
        {
            return 6;
        }

        if (HasFlush(cards))
        {
            return 5;
        }

        if (HasStraight(cards, out var combinationCards))
        {
            return 4;
        }

        if (HasThreeOfAKind(cards))
        {
            return 3;
        }

        if (HasTwoPairs(cards))
        {
            return 2;
        }

        if (HasOnePair(cards))
        {
            return 1;
        }

        return 0;
    }

    private static bool HasRoyalFlesh(Card[] cards)
    {
        var suitCards = cards.GroupBy(c => c.Suit).FirstOrDefault(g => g.Count() > 4)?.OrderBy(c => c.Rank).ToArray();
        if (suitCards == null) return false;
        var royalFlushCards = suitCards.Where(card => card.Rank is >= Rank.Ten and <= Rank.Ace).ToArray();
        return royalFlushCards.Length == 5;
    }

    private static bool HasStraightFlesh(Card[] cards)
    {
        return HasFlush(cards) && HasStraight(cards, out var combo);
    }

    private static bool HasFourOfAKind(Card[] cards)
    {
        var rankCards = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 4)?.ToArray();
        return rankCards != null;
    }

    private static bool HasFullHouse(Card[] cards)
    {
        List<Card> cloneCards = cards.ToList();
        var threeOfAKindCards = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 3)?.ToArray();
        if (threeOfAKindCards == null) return false;
        cloneCards.RemoveAll(c => threeOfAKindCards.Contains(c));
        var pairCards = cloneCards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 2)?.ToArray();
        return pairCards != null;
    }

    private static bool HasFlush(Card[] cards)
    {
        var suitCards = cards.GroupBy(c => c.Suit).FirstOrDefault(g => g.Count() > 4)?.ToArray();
        return suitCards != null;
    }


    private static bool HasStraight(Card[] cards, out Card[] combinationCards)
    {
        combinationCards = new Card[5];
        var sortedCards = cards.OrderBy(c => c.Rank).Distinct().ToArray();
        if (sortedCards.Last().Rank == Rank.Ace &&
            sortedCards.First().Rank == Rank.Two &&
            sortedCards[1].Rank == Rank.Three &&
            sortedCards[2].Rank == Rank.Four &&
            sortedCards[3].Rank == Rank.Five)
        {
            combinationCards[0] = sortedCards.Last();
            combinationCards[1] = sortedCards.First();
            combinationCards[2] = sortedCards[1];
            combinationCards[3] = sortedCards[2];
            combinationCards[4] = sortedCards[3];
            return true;
        }

        for (var i = sortedCards.Length-5; i >= 0; i--)
        {
            if (sortedCards[i + 4].Rank - sortedCards[i].Rank != 4) continue;
            combinationCards[0] = sortedCards[i];
            combinationCards[1] = sortedCards[i + 1];
            combinationCards[2] = sortedCards[i + 2];
            combinationCards[3] = sortedCards[i + 3];
            combinationCards[4] = sortedCards[i + 4];
            return true;
        }

        return false;
    }

    private static bool HasThreeOfAKind(Card[] cards)
    {
        var groupThreeOfAKind = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 3)?.ToArray();
        return groupThreeOfAKind != null;
    }

    private static bool HasTwoPairs(Card[] cards)
    {
        var groupsOfPair = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 2).ToArray();
        return groupsOfPair.Length >= 2;
    }

    private static bool HasOnePair(Card[] cards)
    {
        var groupsOfPair = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 2).ToArray();
        return groupsOfPair.Length == 1;
    }
}