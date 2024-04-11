using ForetoBot.Business.Handlers.Telegram;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace ForetoBot.Api.Controllers;

[ApiController]
[Route("/bot")]
public class BotController(IMessageBus messageBus) : ControllerBase
{
    [HttpPost("hook", Name = "TelegramWebhook")]
    public async Task<IActionResult> Post([FromBody] TgUpdateEvent update)
    {
        await messageBus.PublishAsync(update);
        return Ok();
    }
}