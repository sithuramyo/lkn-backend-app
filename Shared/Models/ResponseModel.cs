using System.Net;
using System.Text.Json.Serialization;

namespace Shared.Models;

public class ResponseModel<T>
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<string>? Errors { get; set; }

    public static ResponseModel<T> Success(T data, string message = "Success")
    {
        return new ResponseModel<T> { IsSuccess = true, Message = message, Data = data };
    }

    public static ResponseModel<T> Created(string message)
    {
        return new ResponseModel<T>
        { IsSuccess = true, Message = $"{message} is created" };
    }

    public static ResponseModel<T> Updated(string message)
    {
        return new ResponseModel<T>
        { IsSuccess = true, Message = $"{message} is updated" };
    }

    public static ResponseModel<T> Deleted(string message)
    {
        return new ResponseModel<T>
        { IsSuccess = true, Message = $"{message} is deleted" };
    }

    public static ResponseModel<T> BadRequest(string message)
    {
        return new ResponseModel<T>
        { IsSuccess = false, Message = message, StatusCode = (int)HttpStatusCode.BadRequest };
    }

    public static ResponseModel<T> Conflict(string message)
    {
        return new ResponseModel<T>
        { IsSuccess = false, Message = message, StatusCode = (int)HttpStatusCode.Conflict };
    }

    public static ResponseModel<T> NotFound(string message)
    {
        return new ResponseModel<T> { IsSuccess = false, Message = message, StatusCode = (int)HttpStatusCode.NotFound };
    }
}