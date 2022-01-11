using System;
using System.Net;

namespace LittleByte.Extensions.AspNet.Responses
{
    public class DeletedResponse<T> : ApiResponse
    {
        public DeletedResponse(Guid entityId)
            : base(HttpStatusCode.NoContent, $"Deleted '{typeof(T).Name}' with ID '{entityId}'") { }
    }
}
