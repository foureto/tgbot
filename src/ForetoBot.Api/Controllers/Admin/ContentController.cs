using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin._Models;
using ForetoBot.Business.Handlers.Admin.Content;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace ForetoBot.Api.Controllers.Admin;

/// <summary>
/// Content management
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("/api/admin/content")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ContentController(IMessageBus mediator) : ControllerBase
{
    /// <summary>
    /// Update text contents
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("text")]
    public Task<AppResult> UpdateText([FromBody] UpdateTextRequest request)
        => mediator.InvokeAsync<AppResult>(request);

    /// <summary>
    /// Update file
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("file")]
    public Task<AppResult<FileRefDto>> UpdateFile([FromForm] UpdateFileRequest request)
        => mediator.InvokeAsync<AppResult<FileRefDto>>(request);
}