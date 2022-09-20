using LittleByte.Common.Objects;

namespace LittleByte.Common.Database
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