using System.Net;
using LittleByte.Core.Objects;

namespace LittleByte.Extensions.AspNet.Responses
{
    public class CreatedResponse<T> : ApiResponse<T> where T : class, IIdObject
    {
        public CreatedResponse(T obj)
            : base(HttpStatusCode.Created, obj, $"Created '{typeof(T).Name}' with ID '{obj.Id}'") { }
    }
}
