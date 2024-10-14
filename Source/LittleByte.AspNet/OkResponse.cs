using System.Net;

namespace LittleByte.AspNet;

public class OkResponse(string message = "") : ApiResponse(HttpStatusCode.OK, message);

public class OkResponse<T>(T obj, string message = "") : ApiResponse<T>(HttpStatusCode.OK, obj, message);