namespace ForetoBot.Business.Handlers.Telegram;

public class MessageHandler
{
    public async Task Handle(TgUpdateEvent anEvent)
    {
        await Task.Yield();
    }
}