using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.AspNet;

[ApiController]
[Authorize]
[Route("[controller]")]
public abstract class Controller : ControllerBase
{
    public Guid? UserId => HttpContext.GetUserId();
}