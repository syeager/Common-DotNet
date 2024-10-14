using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.AspNet;

[ApiController]
[Authorize]
public abstract class Controller : ControllerBase
{
    public Guid? UserId => HttpContext.GetUserId();

    protected static OkResponse Ok(string message = "") => new(message);
    protected static OkResponse<T> Ok<T>(T obj, string message = "") => new(obj, message);
    protected static BadRequestResponse Bad(string message = "") => new(message);
    protected static BadRequestResponse<T> Bad<T>(string message = "") => new(message);
}