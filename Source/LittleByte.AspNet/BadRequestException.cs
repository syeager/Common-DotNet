using System.Net;

namespace LittleByte.AspNet;

public class BadRequestException : HttpException
{
    public BadRequestException(string message, Exception? innerException = null)
        : base(HttpStatusCode.BadRequest, message, innerException) { }
}