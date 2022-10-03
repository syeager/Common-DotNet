namespace LittleByte.Common.Messaging.Serialization;

public interface IMessageSerializer
{
    ReadOnlyMemory<byte> Serialize(object message);
}
