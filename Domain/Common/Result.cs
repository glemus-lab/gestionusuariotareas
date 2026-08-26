namespace Domain.Common;

public class Result
{
    public bool Success { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
    public int StatusCode { get; protected set; }
    public List<string> Errors { get; protected set; } = [];

    protected Result() { }

    public static Result Ok(int statusCode) => new()
    {
        Success = true,
        Message = "Operación exitosa",
        StatusCode = statusCode
    };

    public static Result Ok(string message, int statusCode) => new()
    {
        Success = true,
        Message = message,
        StatusCode = statusCode
    };

    public static Result Fail(string message, int statusCode) => new()
    {
        Success = false,
        Message = message,
        StatusCode = statusCode,
        Errors = []
    };

    public static Result Fail(string message, int statusCode, List<string> errors) => new()
    {
        Success = false,
        Message = message,
        StatusCode = statusCode,
        Errors = errors
    };
}