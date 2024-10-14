using System.Net;

namespace LittleByte.AspNet;

public class BadRequestResponse(string message = "") : ApiResponse(HttpStatusCode.BadRequest, message);

public class BadRequestResponse<T> : ApiResponse<T>
{
    public BadRequestResponse(T? obj, string message = "")
        : base(HttpStatusCode.BadRequest, obj, message) { }

    public BadRequestResponse(string message)
        : base(HttpStatusCode.BadRequest, message) { }
}