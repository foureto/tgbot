using ForetoBot.Api.Services.Telegram;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddTelegram(builder.Configuration)
    .AddSpaStaticFiles(e => e.RootPath = "dist");

var app = builder.Build();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Value = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            })
        .ToArray();
    return forecast;
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
#pragma warning disable ASP0014
app.UseEndpoints(_ => { });
#pragma warning restore ASP0014
app.Use((ctx, next) =>
{
    if (!ctx.Request.Path.StartsWithSegments("/api") &&
        !ctx.Request.Path.StartsWithSegments("/files") &&
        !ctx.Request.Path.StartsWithSegments("/swagger")) return next();
    ctx.Response.StatusCode = 404;
    return Task.CompletedTask;
});

app.UseSpaStaticFiles();
app.UseSpa(spa =>
{
    if (builder.Environment.IsDevelopment())
        spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4000");
});

app.Run();