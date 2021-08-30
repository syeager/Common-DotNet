using System;
using System.Net;

namespace LittleByte.Extensions.AspNet.Responses
{
    public class DeletedResponse<T> : ApiResponse
    {
        public DeletedResponse(Guid entityID)
            : base(HttpStatusCode.NoContent, $"Deleted '{typeof(T).Name}' with ID '{entityID}'") {}
    }
}