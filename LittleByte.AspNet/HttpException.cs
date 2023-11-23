using System.Net;

namespace LittleByte.AspNet;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public HttpException(HttpStatusCode statusCode, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}