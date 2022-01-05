using System;
using System.ComponentModel.DataAnnotations;
using LittleByte.Core.Objects;

namespace LittleByte.Extensions.AspNet.Core
{
    public abstract class Dto : IIdObject
    {
        [Required]
        public Guid Id { get; init; } = Guid.Empty;
    }
}
