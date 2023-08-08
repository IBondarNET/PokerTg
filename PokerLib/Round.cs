namespace PokerLib;

public abstract class Round
{
    public Enums.Rounds StateRound;
     
    public abstract void Start(Lobby lobby, Deck deck);
    public abstract void OptionsAction(Game game, Lobby lobby); // [] Action 
    public abstract void ChoiceAction();
    public void Check()
    {
    }

    // public void Bet()
    // {
    // }

    public void Call()
    {
    }

    public void Raise()
    {
    }
    public void Fold()
    {
    }
}