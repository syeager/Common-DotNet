using System;
using System.Net;

namespace LittleByte.Core.Exceptions
{
    public class ConflictRequestException : HttpException
    {
        public ConflictRequestException(string message, Exception? innerException = null)
            : base(HttpStatusCode.Conflict, message, innerException) { }
    }
}
