using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.AspNet;

[ApiController]
[Authorize]
public abstract class Controller : ControllerBase
{
    public Guid? UserId => HttpContext.GetUserId();

    protected static OkResponse Ok(string message = "") => new(message);
    protected static OkResponse<T> Ok<T>(T obj, string message = "") where T : class => new(obj, message);
}