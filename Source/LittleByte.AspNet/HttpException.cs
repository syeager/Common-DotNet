using System.Net;

namespace LittleByte.AspNet;

public class HttpException(HttpStatusCode statusCode, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}