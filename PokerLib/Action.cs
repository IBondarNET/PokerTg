namespace PokerLib;

public class Action
{
    public ActionCommand ActionCommand { get; set; }
}

public enum ActionCommand
{
    Call,
    Check,
    Fold,
    Raise,
    Bet
}