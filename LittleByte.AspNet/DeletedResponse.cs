using System.Net;

namespace LittleByte.AspNet;

public class DeletedResponse<T> : ApiResponse
{
    public DeletedResponse(Guid entityId)
        : base(HttpStatusCode.NoContent, $"Deleted '{typeof(T).Name}' with ID '{entityId}'") { }
}