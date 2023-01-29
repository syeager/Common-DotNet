namespace LittleByte.Common.Domain;

public readonly struct Id<T> : IEquatable<Id<T>>
{
    public Guid Value { get; }

    public static readonly Id<T> Empty = new(Guid.Empty);

    public Id() => Value = Guid.NewGuid();
    public Id(Guid value) => Value = value;
    public Id(Guid? value = null) => Value = value ?? Guid.NewGuid();

    public static implicit operator Guid(Id<T> id) => id.Value;
    public static implicit operator Guid?(Id<T> id) => id == Empty ? null : id.Value;

    public static bool operator ==(Id<T> left, Id<T> right) => left.Value == right.Value;
    public static bool operator !=(Id<T> left, Id<T> right) => left.Value != right.Value;

    public override string ToString() => Value.ToString();

    public bool Equals(Id<T> other) => Value.Equals(other.Value);

    public override bool Equals(object? obj) => obj is Id<T> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public Id<T> GetNewIfEmpty() => this == Empty ? new Id<T>() : this;
}