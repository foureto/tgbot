namespace ForetoBot.Business.Handlers.Admin._Models;

public class FileRefDto
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string MimeType { get; set; }
    public string FileName { get; set; }
    public string Text { get; set; }
}