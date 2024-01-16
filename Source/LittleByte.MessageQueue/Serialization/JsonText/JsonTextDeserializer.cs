using System.Text.Json;

namespace LittleByte.MessageQueue.Serialization.JsonText;

public sealed class JsonTextDeserializer : IMessageDeserializer
{
    public T? Deserialize<T>(ReadOnlyMemory<byte> bytes)
    {
        var message = JsonSerializer.Deserialize<T>(bytes.Span);
        return message;
    }

    public object? Deserialize(Type type, ReadOnlyMemory<byte> bytes)
    {
        var message = JsonSerializer.Deserialize(bytes.Span, type);
        return message;
    }
}