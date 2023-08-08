namespace PokerLib.Rounds;

public class FlopRound : Round
{
    public FlopRound()
    {
        StateRound = Enums.Rounds.Flop;
    }
    
    public override void Start(Lobby lobby, Deck deck)
    {
        deck.CardsOnTable.Add(deck.GetShuffleCard());
        deck.CardsOnTable.Add(deck.GetShuffleCard());
        deck.CardsOnTable.Add(deck.GetShuffleCard());
    }

    public override void OptionsAction(Game game, Lobby lobby)
    {
        throw new NotImplementedException();
    }

    public override void ChoiceAction()
    {
        throw new NotImplementedException();
    }

}