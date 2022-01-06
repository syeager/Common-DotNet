using System.Net;

namespace LittleByte.Extensions.AspNet.Responses
{
    public class OkResponse<T> : ApiResponse<T> where T : class
    {
        public OkResponse(T obj, string? message = null)
            : base(HttpStatusCode.OK, obj, message) { }
    }
}
