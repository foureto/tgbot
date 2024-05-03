using Microsoft.AspNetCore.Mvc;
using IResult = ForetoBot.Business.Commons.Models.IResult;

namespace ForetoBot.Api.Extensions;

internal static class ApiExtensions
{
    public static IActionResult Respond(this ControllerBase controllerBase, IResult result)
        => controllerBase.StatusCode(result.StatusCode, result);
}