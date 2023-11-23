using System.ComponentModel.DataAnnotations;
using LittleByte.Common.Objects;

namespace LittleByte.Common.AspNet.Core
{
    public abstract class Dto : IIdObject
    {
        [Required]
        public Guid Id { get; init; } = Guid.Empty;
    }
}
