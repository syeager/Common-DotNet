using System.Net;

namespace LittleByte.Common.AspNet.Responses
{
    public class OkResponse : ApiResponse
    {
        public OkResponse(HttpStatusCode statusCode, string? message = null)
            : base(statusCode, message)
        {
        }
    }

    public class OkResponse<T> : ApiResponse<T> where T : class
    {
        public OkResponse(T obj, string? message = null)
            : base(HttpStatusCode.OK, obj, message) { }
    }
}