using System.Runtime.Serialization;

namespace ForetoBot.Business.Commons.Models;

[DataContract]
public class PagedAppResult<T> : IAppResult<IEnumerable<T>>
{
    [DataMember(Order = 3)] public int Count { get; set; }
    [DataMember(Order = 4)] public int Page { get; set; }
    [DataMember(Order = 5)] public int Total { get; set; }
    [DataMember(Order = 6)] public IEnumerable<T> Data { get; set; }
    [DataMember(Order = 1)] public bool Success { get; set; }
    [DataMember(Order = 2)] public string Message { get; set; }
    [DataMember(Order = 7)] public int StatusCode { get; set; }

    public static PagedAppResult<T> Ok(IEnumerable<T> data, int count = 0, int page = 0, int total = 0)
    {
        return New(null, 200, data, true, count, page, total);
    }

    public static PagedAppResult<T> Created(IEnumerable<T> data, int count = 0, int page = 0, int total = 0)
    {
        return New(null, 201, data, true, count, page, total);
    }

    public static PagedAppResult<T> Updated(IEnumerable<T> data = default, int count = 0, int page = 0, int total = 0)
    {
        return New(null, 204, data, true, count, page, total);
    }

    public static PagedAppResult<T> Bad(string message)
    {
        return New(message, 400);
    }

    public static PagedAppResult<T> Forbidden(string message)
    {
        return New(message, 401);
    }

    public static PagedAppResult<T> UnAuthorized(string message)
    {
        return New(message, 403);
    }

    public static PagedAppResult<T> NotFound(string message)
    {
        return New(message, 404);
    }

    public static PagedAppResult<T> Failed(string message, int code = 500)
    {
        return New(message, code);
    }

    public static PagedAppResult<T> Failed(IAppResult appResult)
    {
        return New(appResult.Message, appResult.StatusCode);
    }

    public static PagedAppResult<T> Internal(string message)
    {
        return New(message);
    }


    private static PagedAppResult<T> New(
        string message,
        int code = 500,
        IEnumerable<T> data = default,
        bool success = false,
        int count = 0,
        int page = 0,
        int total = 0)
    {
        return new()
        {
            Message = message,
            StatusCode = code,
            Success = success,
            Data = data,
            Count = count,
            Page = page,
            Total = total,
        };
    }
}