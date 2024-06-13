namespace ForetoBot.Business.Services.FileStore.Models;

public class SaveFileRequest
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public string SubPath { get; set; }
}