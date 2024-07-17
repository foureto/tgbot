using ForetoBot.DataAccess.Domain.References;
using Microsoft.EntityFrameworkCore;

namespace ForetoBot.DataAccess.Repos.Internals;

internal class ContentRepository(AppDbContext context) : BaseRepository<StoredText>(context), IContentRepository
{
    public Task<StoredFile> GetFile(Guid fileId, CancellationToken ct)
        => context.Files.FirstOrDefaultAsync(e => e.Id == fileId, ct);

    public async Task<StoredFile> AddFile(StoredFile fileContent, CancellationToken ct)
    {
        context.Add(fileContent);
        await context.SaveChangesAsync(ct);
        return fileContent;
    }
}