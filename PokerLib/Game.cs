using PokerLib.Rounds;

namespace PokerLib;

public class Game
{
    private readonly Lobby _lobby;
    private readonly Deck _deck;
    private readonly Round[] _rounds;
    public decimal Pot;

    public Game(Lobby lobby)
    {
        _lobby = lobby;
        _deck = new Deck();
        _rounds = new Round[]
        {
            new PreFlopRound(),
            new FlopRound(),
            new TurnRound(),
            new RiverRound()
        };
    }

    public void StartGame()
    {
        foreach (var round in _rounds)
        {
            round.Start(_lobby, _deck);
            //
            
            
        }
        _deck.ClearDeck();
    }

}

// public record struct Card(Colors Color, Cards Cards);