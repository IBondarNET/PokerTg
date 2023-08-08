namespace PokerLib.Rounds;

public class TurnRound: Round
{
    public TurnRound()
    {
        StateRound = Enums.Rounds.Turn;
    }
    public override void Start(Lobby lobby, Deck deck)
    {
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