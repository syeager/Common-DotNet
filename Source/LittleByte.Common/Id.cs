namespace LittleByte.Common;

public readonly struct Id<T>(Guid value) : IEquatable<Id<T>>
{
    public Guid Value { get; } = value;

    public static readonly Id<T> Empty = new(Guid.Empty);

    public Id()
        : this(Guid.NewGuid()) { }

    public static implicit operator Guid(Id<T> id) => id.Value;
    public static implicit operator Guid?(Id<T> id) => id == Empty ? null : id.Value;

    public static bool operator ==(Id<T> left, Id<T> right) => left.Equals(right);
    public static bool operator !=(Id<T> left, Id<T> right) => !left.Equals(right);

    public override string ToString() => Value.ToString();

    public bool Equals(Id<T> other) => Value.Equals(other.Value);

    public override bool Equals(object? obj) => obj is Id<T> other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();

    public Id<T> GetNewIfEmpty() => this == Empty ? new Id<T>() : this;
}