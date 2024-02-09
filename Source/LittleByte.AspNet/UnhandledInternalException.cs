using System.Net;

namespace LittleByte.AspNet;

public sealed class UnhandledInternalException(string message = "Unexpected server error") : HttpException(HttpStatusCode.InternalServerError, message);