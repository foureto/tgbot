using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin.Files;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace ForetoBot.Api.Controllers.Admin;

[ApiController]
[Route("/admin/store")]
public class StoreController(IMessageBus messageBus) : ControllerBase
{
    [HttpPost]
    public Task<AppResult> StoreFile([FromForm] StoreFileRequest request)
        => messageBus.InvokeAsync<AppResult>(request);

    [HttpDelete]
    public Task<AppResult> StoreFile([FromBody] RemoveStoredFile request)
        => messageBus.InvokeAsync<AppResult>(request);
}