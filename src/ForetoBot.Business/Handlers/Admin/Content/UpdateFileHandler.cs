using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin._Models;
using ForetoBot.Business.Services.FileStore;
using ForetoBot.Business.Services.FileStore.Models;
using ForetoBot.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace ForetoBot.Business.Handlers.Admin.Content;

public class UpdateFileRequest
{
    public Guid FileId { get; set; }
    public IFormFile File { get; set; }
}

public class UpdateFileHandler(IUnitOfWork unitOfWork, IFileStore fileStore)
{
    public async Task<AppResult<FileRefDto>> Handle(UpdateFileRequest request, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.Content.GetFile(request.FileId, cancellationToken);
        if (file == null) return AppResult<FileRefDto>.NotFound("File not found");

        var storeRequest = new SaveFileRequest
        {
            MimeType = request.File.ContentType,
            FileStream = request.File.OpenReadStream(),
            FileName = $"{Guid.NewGuid():N}{new FileInfo(request.File.FileName).Extension}",
            SubPath = "upl"
        };

        var newFile = await fileStore.Save(storeRequest, cancellationToken);

        file.FileName = newFile.FileName;
        file.MimeType = newFile.MimeType;
        file.Url = newFile.Url;
        file.Updated = DateTimeOffset.UtcNow;

        await unitOfWork.Save(cancellationToken);

        return AppResult<FileRefDto>.Ok(file.Adapt<FileRefDto>());
    }
}