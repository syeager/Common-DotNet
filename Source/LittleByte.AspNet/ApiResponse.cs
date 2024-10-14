using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.AspNet;

public class ApiResponse : IActionResult
{
    [Required]
    public int StatusCode { get; }
    [Required]
    public bool IsError { get; }
    [Required]
    public string Message { get; }

    public ApiResponse(HttpStatusCode statusCode, string message = "")
    {
        StatusCode = (int) statusCode;
        IsError = StatusCode >= 400;
        Message = message;
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        return context.HttpContext.Response.WriteJsonAsync(this, StatusCode);
    }
}

public class ApiResponse<T>(HttpStatusCode statusCode, T? obj, string message = "") : ApiResponse(statusCode, message)
{
    public T? Obj { get; } = obj;

    public ApiResponse(HttpStatusCode statusCode, string message)
        : this(statusCode, default, message) { }
}