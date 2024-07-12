using System.ComponentModel.DataAnnotations;

namespace ForetoBot.DataAccess.Domain.References;

public class StoredFile : BaseEntity
{
    public Guid Id = Guid.NewGuid();
    [MaxLength(255)] public string Url { get; set; }
    [MaxLength(255)] public string MimeType { get; set; }
}