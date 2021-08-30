using System;

namespace LittleByte.Domain
{
    public readonly struct DomainGuid<T> : IEquatable<DomainGuid<T>>
    {
        public Guid Value { get; }

        public static readonly DomainGuid<T> Empty = new(Guid.Empty);

        internal DomainGuid(Guid value) => Value = value;
        internal DomainGuid(Guid? value = null) => Value = value == null ? Guid.NewGuid() : value.Value;

        public static implicit operator Guid(DomainGuid<T> id) => id.Value;
        public static implicit operator DomainGuid<T>(Guid guid) => new(guid);

        public static implicit operator Guid?(DomainGuid<T> id) => id == Empty ? null : id.Value;
        public static implicit operator DomainGuid<T>?(Guid? guid) => guid == null ? null : new DomainGuid<T>(guid);

        public static bool operator ==(DomainGuid<T> left, DomainGuid<T> right) => left.Equals(right);
        public static bool operator !=(DomainGuid<T> left, DomainGuid<T> right) => !left.Equals(right);

        public override string ToString() => Value.ToString();

        public bool Equals(DomainGuid<T> other) => Value.Equals(other.Value);

        public override bool Equals(object? obj) => obj is DomainGuid<T> other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public DomainGuid<T> GetNewIfEmpty() => this == Empty ? new DomainGuid<T>() : this;
    }
}