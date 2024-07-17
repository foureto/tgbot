using ForetoBot.DataAccess.Domain.References;

namespace ForetoBot.DataAccess.Repos;

public interface IContentRepository : IBaseRepository<StoredText>
{
    Task<StoredFile> GetFile(Guid fileId, CancellationToken ct);
    Task<StoredFile> AddFile(StoredFile storedFile, CancellationToken ct);
}