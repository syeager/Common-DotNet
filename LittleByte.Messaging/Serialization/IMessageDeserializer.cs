﻿namespace LittleByte.Messaging;

public interface IMessageDeserializer
{
    T? Deserialize<T>(ReadOnlyMemory<byte> bytes);
    object? Deserialize(Type type, ReadOnlyMemory<byte> bytes);
}