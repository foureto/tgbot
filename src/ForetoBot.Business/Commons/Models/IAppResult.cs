namespace ForetoBot.Business.Commons.Models;

public interface IAppResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

public interface IAppResult<T> : IAppResult
{
    T Data { get; set; }
}