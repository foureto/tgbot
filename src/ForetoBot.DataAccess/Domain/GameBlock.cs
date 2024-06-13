using System.ComponentModel.DataAnnotations;

namespace ForetoBot.DataAccess.Domain;

public class GameBlock
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(255)] public string Name { get; set; }
}