using Flour.YandexSpeechKit;
using ForetoBot.Api.Extensions;
using ForetoBot.Business.Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForetoBot.Api.Controllers;

[ApiController]
[Route("/api/data")]
public class SampleController(ISpeechKitService synth) : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
        => this.Respond(AppResultList<object>.Ok(Enumerable.Range(1, 10).Select(e => new {one = e})));

    [HttpGet("text2speech")]
    public async Task<IActionResult> TextToSpeech()
    {
        var test = await synth.GenerateSpeech("qweqw");
        return File(test, "audio/ogg", Guid.NewGuid().ToString("N"));
    }
}