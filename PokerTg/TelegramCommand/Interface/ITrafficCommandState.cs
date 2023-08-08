namespace PokerTg.TelegramCommand.Interface;

public interface ITrafficCommandState
{
    public void HandleState(ITrafficCommandState commandState);
}