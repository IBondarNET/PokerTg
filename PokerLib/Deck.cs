using PokerLib.Enums;

namespace PokerLib;

public class Deck
{
    private List<Card> _allCards  = new List<Card>();
    public readonly Dictionary<Player, Card[]> CardsInHandPlayer = new Dictionary<Player, Card[]>();
    public readonly List<Card> CardsOnTable = new List<Card>();
    public int Pot;
    public Deck()
    {
        CreateNewCardDesk();
    }
    private void CreateNewCardDesk()
    {
        _allCards = Enum.GetValues<Suits>()
            .SelectMany(color => Enum.GetValues<Rank>()
                .Select(card => new Card(color, card)))
            .ToList();
    }

    public Card GetShuffleCard()
    {
        var random = new Random();
        var index = random.Next(_allCards.Count);
        var card = _allCards[index];
        _allCards.RemoveAt(index);
        return card;
    }
    public void ClearDeck()
    {
        CreateNewCardDesk();
        CardsInHandPlayer.Clear();
        CardsOnTable.Clear();
        Pot = 0;
    }
}