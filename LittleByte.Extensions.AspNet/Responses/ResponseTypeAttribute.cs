using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.Extensions.AspNet.Responses
{
    public class ResponseTypeAttribute : ProducesResponseTypeAttribute
    {
        public ResponseTypeAttribute(HttpStatusCode statusCode)
            : base(typeof(ApiResponse), (int)statusCode) { }

        public ResponseTypeAttribute(HttpStatusCode statusCode, Type dtoType)
            : base(typeof(ApiResponse<>).MakeGenericType(dtoType), (int)statusCode) { }
    }
}
