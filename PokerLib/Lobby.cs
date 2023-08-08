namespace PokerLib;

public class Lobby
{ 
    public readonly decimal BuyIn;
    public readonly int MaxCountPlayers;
    public readonly bool IsFreeLobby;
    public readonly int StartChips;
    public readonly decimal BigBlind;
    public decimal PrizePool { get; private set; } = decimal.Zero;
    public List<Player> Players { get;} = new List<Player>();

    public Lobby(int maxCountPlayers, int startChips, int buyIn = 0, bool isFreeLobby = true)
    {
        MaxCountPlayers = maxCountPlayers;
        StartChips = startChips;
        BuyIn = buyIn;
        IsFreeLobby = isFreeLobby;
        BigBlind = decimal.Parse(startChips.ToString()) / 100;
    }

    public void AddPlayer(Player player)
    {
        if (Players.Count >= MaxCountPlayers)
        {
            throw new Exception("Lobby is full");
        }

        if (BuyIn > player.Balance)
        {
            throw new Exception("Not enough money");
        }

        if (!IsFreeLobby)
        {
            PrizePool += BuyIn;
        }
        player.ChipsInLobby.Add(this,StartChips);
        player.DepositToLobby(BuyIn);
        Players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        if (!IsFreeLobby)
        {
            var money = player.ChipsInLobby[this]/(StartChips / BuyIn);
            player.WithdrawFromLobby(money);
        }
        player.ChipsInLobby.Remove(this);
        Players.Remove(player);
    }
}