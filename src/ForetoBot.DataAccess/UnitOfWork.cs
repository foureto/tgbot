using ForetoBot.DataAccess.Repos;
using ForetoBot.DataAccess.Repos.Internals;

namespace ForetoBot.DataAccess;

internal class UnitOfWork(AppDbContext context) : IUnitOfWork, IAsyncDisposable
{
    private readonly Lazy<IContentRepository> _content = new(() => new ContentRepository(context));
    private readonly Lazy<IDomansRepository> _doman = new(() => new DomansRepository(context));

    public IContentRepository Content => _content.Value;
    public IDomansRepository Doman => _doman.Value;


    public async Task<bool> Save(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token) > 0;
    }

    public async ValueTask DisposeAsync()
    {
        if (context != null) await context.DisposeAsync();
    }
}