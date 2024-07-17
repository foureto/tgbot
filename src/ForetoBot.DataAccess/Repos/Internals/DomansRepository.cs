using ForetoBot.DataAccess.Domain.Games;

namespace ForetoBot.DataAccess.Repos.Internals;

internal class DomansRepository(AppDbContext context) : BaseRepository<DomanCategory>(context), IDomansRepository
{
}