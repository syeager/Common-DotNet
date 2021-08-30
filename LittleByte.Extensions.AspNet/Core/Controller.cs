using System;
using LittleByte.Extensions.AspNet.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleByte.Extensions.AspNet.Core
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public abstract class Controller : ControllerBase
    {
        public Guid? UserId => HttpContext.GetUserID();
    }
}