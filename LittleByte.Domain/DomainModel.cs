using System;
using LittleByte.Core.Objects;

namespace LittleByte.Domain
{
    public abstract class DomainModel<T> : IIdObject
    {
        public Id<T> Id { get; }
        Guid IIdObject.Id => Id.Value;

        protected DomainModel(Id<T> id)
        {
            Id = id;
        }
    }
}
