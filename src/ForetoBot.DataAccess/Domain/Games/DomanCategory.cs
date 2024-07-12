using ForetoBot.DataAccess.Domain.References;

namespace ForetoBot.DataAccess.Domain.Games;

public class DomanCategory : BaseEntity<int>
{
    public StoredText Name { get; set; }
    public StoredText Description { get; set; }
    public StoredFile Label { get; set; }
}