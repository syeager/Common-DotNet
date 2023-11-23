using System.Net;

namespace LittleByte.AspNet;

public class BadRequestResponse : ApiResponse
{
    public BadRequestResponse(string message = "")
        : base(HttpStatusCode.BadRequest, message) { }
}

public class BadRequestResponse<T> : ApiResponse<T>
    where T : class
{
    public BadRequestResponse(T? obj, string message = "")
        : base(HttpStatusCode.BadRequest, obj, message) { }

    public BadRequestResponse(string message)
        : base(HttpStatusCode.BadRequest, message) { }
}