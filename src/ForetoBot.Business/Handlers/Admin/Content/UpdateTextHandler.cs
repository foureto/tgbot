using FastExpressionCompiler;
using ForetoBot.Business.Commons.Models;
using ForetoBot.DataAccess;

namespace ForetoBot.Business.Handlers.Admin.Content;

public class UpdateTextRequest
{
    public Guid TextId { get; set; }
    public Dictionary<string, string> Texts { get; set; } = new();
}

public class UpdateTextHandler(IUnitOfWork unitOfWork)
{
    public async Task<AppResult> Handle(UpdateTextRequest request, CancellationToken cancellationToken)
    {
        var content = await unitOfWork.Content.Get(e => e.Id == request.TextId, cancellationToken);
        if (content == null) return AppResult.Ok("Content not found");

        foreach (var (locale, text) in request.Texts ?? new Dictionary<string, string>())
            content.TrySet(locale, text);

        content.Updated = DateTimeOffset.UtcNow;
        await unitOfWork.Save(cancellationToken);

        return AppResult.Ok("Text updated");
    }
}