using ForetoBot.Api.Extensions;
using ForetoBot.Business.Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForetoBot.Api.Controllers;

[ApiController]
[Route("/api/data")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
        => this.Respond(ResultList<object>.Ok(Enumerable.Range(1, 10).Select(e => new { one = e })));
}