using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Services.FileStore;
using ForetoBot.Business.Services.FileStore.Models;

namespace ForetoBot.Business.Handlers.Admin.Files;

public record RemoveStoredFile(string FilePath);

public class RemoveFileHandler(IFileStore fileStore)
{
    public async Task<AppResult> Handle(RemoveStoredFile request)
    {
        await fileStore.RemoveFile(new RemoveFileRequest { FilePath = request.FilePath });
        return AppResult.Ok();
    }
}