namespace LittleByte.Messaging.Serialization;

public interface IMessageSerializer
{
    ReadOnlyMemory<byte> Serialize(object message);
}
