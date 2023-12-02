using System.Net;

namespace LittleByte.AspNet;

public class OkResponse : ApiResponse
{
    public OkResponse(string message = "")
        : base(HttpStatusCode.OK, message) { }
}

public class OkResponse<T> : ApiResponse<T>
    where T : class
{
    public OkResponse(T obj, string message = "")
        : base(HttpStatusCode.OK, obj, message) { }
}