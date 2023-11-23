using System.Net;

namespace LittleByte.AspNet;

public class ConflictRequestException : HttpException
{
    public ConflictRequestException(string message, Exception? innerException = null)
        : base(HttpStatusCode.Conflict, message, innerException) { }
}