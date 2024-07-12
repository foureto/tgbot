using System.ComponentModel.DataAnnotations;

namespace ForetoBot.DataAccess.Domain.References;

public class StoredText : BaseEntity
{
    public Guid Id = Guid.NewGuid();
    [MaxLength(512)] public string DefaultText { get; set; } = string.Empty;
    [MaxLength(512)] public string Ru { get; set; } = string.Empty;
    [MaxLength(512)] public string En { get; set; } = string.Empty;
}