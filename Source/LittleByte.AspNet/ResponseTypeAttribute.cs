using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.AspNet;

public class ResponseTypeAttribute : ProducesResponseTypeAttribute
{
    public ResponseTypeAttribute(HttpStatusCode statusCode)
        : base(typeof(ApiResponse), (int) statusCode) { }

    public ResponseTypeAttribute(HttpStatusCode statusCode, Type dtoType)
        : base(typeof(ApiResponse<>).MakeGenericType(dtoType), (int) statusCode) { }
}

public class ResponseTypeAttribute<T>(HttpStatusCode statusCode) : ResponseTypeAttribute(statusCode, typeof(T));