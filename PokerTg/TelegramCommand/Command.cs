using PokerTg.TelegramCommand.Interface;

namespace PokerTg.TelegramCommand;

public abstract class Command : ITrafficCommandState
{
    public void HandleState(ITrafficCommandState commandState)
    {
    }
}