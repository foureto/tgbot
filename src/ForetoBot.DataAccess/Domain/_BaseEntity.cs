namespace ForetoBot.DataAccess.Domain;

public class BaseEntity
{
    public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
}

public class BaseEntity<T> : BaseEntity
{
    public T Id { get; set; }
}