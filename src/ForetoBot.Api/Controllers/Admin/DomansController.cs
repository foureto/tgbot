using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin.Domans;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace ForetoBot.Api.Controllers.Admin;

[ApiController]
[Route("/api/admin/domans")]
public class DomansController(IMessageBus messageBus) : ControllerBase
{
    [HttpGet]
    public Task<AppResultList<DomanCategoryDto>> GetCategories()
        => messageBus.InvokeAsync<AppResultList<DomanCategoryDto>>(new GetDomanCategories());
}