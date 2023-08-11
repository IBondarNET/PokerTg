namespace PokerLib.Rounds;

public class PreFlopRound : Round
{
    public PreFlopRound()
    {
        StateRound = Enums.Rounds.PreFlop;
    }

    public override void Start( Lobby lobby, Deck deck)
    {
        foreach (var player in lobby.Players)
        {
            deck.CardsInHandPlayer.Add(player, new Card[]
            {
                    deck.GetShuffleCard(),
                    deck.GetShuffleCard()
            });
        }
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