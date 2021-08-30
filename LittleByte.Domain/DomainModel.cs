namespace LittleByte.Domain
{
    public abstract class DomainModel<T>
    {
        public DomainGuid<T> Id { get; }

        protected DomainModel(DomainGuid<T> id)
        {
            Id = id;
        }
    }
}