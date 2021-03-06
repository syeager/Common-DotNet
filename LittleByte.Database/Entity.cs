using System;
using LittleByte.Core.Objects;

namespace LittleByte.Database
{
    public interface IEntity<out T> : IIdObject
    {
        T ToDomain();
    }

    public abstract class Entity<T> : IEntity<T>
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public abstract T ToDomain();
    }
}