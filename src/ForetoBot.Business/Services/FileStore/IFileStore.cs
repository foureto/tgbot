using ForetoBot.Business.Services.FileStore.Models;

namespace ForetoBot.Business.Services.FileStore;

public interface IFileStore
{
    Task<StoredFile> Save(SaveFileRequest request, CancellationToken token = default);
    Task RemoveFile(RemoveFileRequest request, CancellationToken token = default);
}