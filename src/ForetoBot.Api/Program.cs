using Flour.Logging;
using ForetoBot.Business;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseLogging()
    .UseWolverine(opts => opts.ApplicationAssembly = typeof(BusinessInjections).Assembly);

builder.Services
    .AddControllers().Services
    .AddBusiness(builder.Configuration)
    .AddHttpClient()
    .AddSpaStaticFiles(e => e.RootPath = "dist");

var app = builder.Build();

app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();
#pragma warning disable ASP0014
app.UseEndpoints(_ => { });
#pragma warning restore ASP0014
app.Use((ctx, next) =>
{
    if (!ctx.Request.Path.StartsWithSegments("/api") &&
        !ctx.Request.Path.StartsWithSegments("/files") &&
        !ctx.Request.Path.StartsWithSegments("/admin") &&
        !ctx.Request.Path.StartsWithSegments("/api-docs") &&
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

app.MapControllers();

app.Run();