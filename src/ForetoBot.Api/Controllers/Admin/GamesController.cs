using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin.Domans;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace ForetoBot.Api.Controllers.Admin;

[ApiController]
[Route("/api/admin/games")]
[ApiExplorerSettings(IgnoreApi = true)]
public class GamesController(IMessageBus messageBus) : ControllerBase
{
    [HttpGet("doman/categories")]
    public Task<AppResultList<DomanCategoryDto>> GetCategories()
        => messageBus.InvokeAsync<AppResultList<DomanCategoryDto>>(new GetDomanCategories());
    
    [HttpPost("doman/categories")]
    public Task<AppResult<int>> CreateCategory([FromBody] CreateCategory request)
        => messageBus.InvokeAsync<AppResult<int>>(request);
}