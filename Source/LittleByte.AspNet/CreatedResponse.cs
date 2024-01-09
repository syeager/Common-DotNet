using System.Net;
using LittleByte.Common;

namespace LittleByte.AspNet;

public sealed class CreatedResponse<T>(T obj)
    : ApiResponse<T>(HttpStatusCode.Created, obj, $"Created '{typeof(T).Name}' with ID '{obj.Id}'")
    where T : class, IIdObject;