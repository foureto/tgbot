using System.ComponentModel.DataAnnotations;

namespace ForetoBot.DataAccess.Domain.References;

public class StoredText : BaseEntity<Guid>
{
    public StoredText()
    {
        Id = Guid.NewGuid();
    }
    
    [MaxLength(512)] public string Ru { get; set; } = string.Empty;
    [MaxLength(512)] public string En { get; set; } = string.Empty;
    
    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { nameof(En).ToLower(), En },
            { nameof(Ru).ToLower(), Ru },
        };
    }

    public bool TrySet(string locale, string text)
    {
        if (string.IsNullOrWhiteSpace(locale) || locale.Length > 3)
            return false;

        var prop = typeof(StoredText).GetProperties().FirstOrDefault(
            e => e.Name.Equals(locale, StringComparison.OrdinalIgnoreCase));
        if (prop is null)
            return false;

        if (prop.GetValue(this)?.ToString() == text)
            return false;

        prop.SetValue(this, text);
        return true;
    }

    public string this[string locale]
    {
        get
        {
            var translation = locale?.ToLower() switch
            {
                "ru" => Ru,
                _ => En
            };
            return string.IsNullOrEmpty(translation) ? En : translation;
        }
    }
}