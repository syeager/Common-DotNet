using System.Net;

namespace LittleByte.Common.AspNet.Responses;

public class DeletedResponse<T> : ApiResponse
{
    public DeletedResponse(Guid entityId)
        : base(HttpStatusCode.NoContent, $"Deleted '{typeof(T).Name}' with ID '{entityId}'") { }
}