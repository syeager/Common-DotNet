using System.Text.Json;

namespace LittleByte.Messaging;

public class JsonTextSerializer : IMessageSerializer
{
    public ReadOnlyMemory<byte> Serialize(object message)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(message);
        return bytes;
    }
}