using System;

namespace LittleByte.Core.Objects
{
    public interface IIdObject
    {
        public Guid Id { get; }
    }

    public interface IStringId
    {
        public string Id { get; }
    }
}