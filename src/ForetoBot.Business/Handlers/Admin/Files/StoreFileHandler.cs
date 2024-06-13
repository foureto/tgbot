using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Services.FileStore;
using ForetoBot.Business.Services.FileStore.Models;
using Microsoft.AspNetCore.Http;

namespace ForetoBot.Business.Handlers.Admin.Files;

public class StoreFileRequest
{
    public IFormFile File { get; set; }
}

public class StoreFileHandler(IFileStore fileStore)
{
    public async Task<AppResult> Handle(StoreFileRequest request)
    {
        var res = await fileStore.Save(new SaveFileRequest
        {
            FileName = request.File.FileName,
            FileStream = request.File.OpenReadStream(),
            MimeType = request.File.ContentType,
            SubPath = "akid/test"
        });

        return AppResult.Ok("File stored");
    }
}