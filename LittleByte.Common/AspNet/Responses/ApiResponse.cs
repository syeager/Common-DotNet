﻿using System.Net;
using LittleByte.Common.AspNet.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.Common.AspNet.Responses;

public class ApiResponse : IActionResult
{
    public bool IsError { get; }
    public string? Message { get; }
    public int StatusCode { get; }

    public ApiResponse(HttpStatusCode statusCode, string? message = null)
    {
        StatusCode = (int)statusCode;
        IsError = StatusCode >= 400;
        Message = message;
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        return context.HttpContext.Response.WriteJsonAsync(this, StatusCode);
    }
}

public class ApiResponse<T> : ApiResponse where T : class
{
    public T? Obj { get; }

    public ApiResponse(HttpStatusCode statusCode, T? obj, string? message = null)
        : base(statusCode, message)
    {
        Obj = obj;
    }

    public ApiResponse(HttpStatusCode statusCode, string message)
        : this(statusCode, null, message) { }
}