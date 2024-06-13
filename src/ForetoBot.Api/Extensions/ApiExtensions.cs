using ForetoBot.Business.Commons.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForetoBot.Api.Extensions;

internal static class ApiExtensions
{
    public static IActionResult Respond(this ControllerBase controllerBase, IAppResult appResult)
        => controllerBase.StatusCode(appResult.StatusCode, appResult);

}