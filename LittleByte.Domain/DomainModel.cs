namespace LittleByte.Domain
{
    public abstract class DomainModel<T>
    {
        public Id<T> Id { get; }

        protected DomainModel(Id<T> id)
        {
            Id = id;
        }
    }
}
