namespace Domain.Common;

public class Result<T> : Result
{
    public T? Data { get; private set; }

    private Result() { }
    public static Result<T> Ok(T data, int statusCode) => new()
    {
        Success = true,
        Message = "Operación exitosa",
        Data = data,
        StatusCode = statusCode
    };

    public static Result<T> Ok(T data, string message, int statusCode) => new()
    {
        Success = true,
        Message = message,
        Data = data,
        StatusCode = statusCode
    };

    public static new Result<T> Fail(string message, int statusCode) => new()
    {
        Success = false,
        Message = message,
        StatusCode = statusCode,
        Errors = []
    };

    public static new Result<T> Fail(string message, int statusCode, List<string> errors) => new()
    {
        Success = false,
        Message = message,
        StatusCode = statusCode,
        Errors = errors
    };
}
