using System.ComponentModel.DataAnnotations;

namespace ForetoBot.DataAccess.Domain.References;

public class StoredFile : BaseEntity<Guid>
{
    public StoredFile()
    {
        Id = Guid.NewGuid();
    }
    
    [MaxLength(255)] public string Url { get; set; }
    [MaxLength(255)] public string MimeType { get; set; }
    [MaxLength(255)] public string FileName { get; set; }
    [MaxLength(512)] public string Text { get; set; }
}