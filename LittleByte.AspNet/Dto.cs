using System.ComponentModel.DataAnnotations;
using LittleByte.Common;

namespace LittleByte.AspNet;

public abstract class Dto : IIdObject
{
    [Required]
    public Guid Id { get; init; } = Guid.Empty;
}