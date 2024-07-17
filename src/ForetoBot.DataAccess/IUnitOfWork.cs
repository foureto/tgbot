using ForetoBot.DataAccess.Repos;

namespace ForetoBot.DataAccess;

public interface IUnitOfWork
{
    IContentRepository Content { get; }
    IDomansRepository Doman { get; }

    Task<bool> Save(CancellationToken token = default);
}