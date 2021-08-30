using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LittleByte.Core.Objects;

namespace LittleByte.Extensions.AspNet.Core
{
    public abstract class Dto : IIdObject
    {
        [Required]
        public Guid Id { get; set; } = Guid.Empty;

        [UsedImplicitly]
        protected Dto()
        {
        }

        protected Dto(Guid id)
        {
            Id = id;
        }
    }
}