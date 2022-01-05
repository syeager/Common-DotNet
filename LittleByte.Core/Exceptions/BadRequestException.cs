using System;
using System.Net;

namespace LittleByte.Core.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string message, Exception? innerException = null)
            : base(HttpStatusCode.BadRequest, message, innerException) { }
    }
}
