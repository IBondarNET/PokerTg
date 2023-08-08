namespace PokerLib;

public class Player
{
    public string Name;
    public readonly Dictionary<Lobby,decimal> ChipsInLobby = new Dictionary<Lobby, decimal>();
    public decimal Balance { get; private set; } = 0;
    public Player(string name)
    {
        Name = name;
    }

    public void DepositToLobby(decimal buyIn)
    {
        Balance -= buyIn;
    }

    public void WithdrawFromLobby(decimal money)
    {
        Balance += money;
    }
}