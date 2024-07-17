using ForetoBot.DataAccess.Domain.References;

namespace ForetoBot.DataAccess.Domain.Games;

public class DomanCard : BaseEntity<int>
{
    public StoredText Title { get; set; } = new();
    public StoredFile Image { get; set; } = new();
    public StoredFile TitleSound { get; set; } = new();
    public StoredFile DescriptionSound { get; set; } = new();
}