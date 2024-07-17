using ForetoBot.DataAccess.Domain.References;

namespace ForetoBot.DataAccess.Domain.Games;

public class DomanCategory : BaseEntity<int>
{
    public int Order { get; set; }
    public StoredText Name { get; set; } = new();
    public StoredText Description { get; set; } = new();
    public StoredFile Label { get; set; } = new();
    public List<DomanCard> Cards { get; set; } = new();
}