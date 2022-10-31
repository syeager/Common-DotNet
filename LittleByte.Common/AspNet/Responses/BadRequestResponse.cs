using System.Net;

namespace LittleByte.Common.AspNet.Responses;

public class BadRequestResponse : ApiResponse
{
    public BadRequestResponse(string? message = null) : base(HttpStatusCode.BadRequest, message) { }
}

public class BadRequestResponse<T> : ApiResponse<T> where T : class
{
    public BadRequestResponse(T? obj, string? message = null) : base(HttpStatusCode.BadRequest, obj, message) { }
    public BadRequestResponse(string message) : base(HttpStatusCode.BadRequest, message) { }
}