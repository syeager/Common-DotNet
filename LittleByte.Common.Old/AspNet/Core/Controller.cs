using LittleByte.Common.AspNet.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.Common.AspNet.Core
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class Controller : ControllerBase
    {
        public Guid? UserId => HttpContext.GetUserId();
    }
}
