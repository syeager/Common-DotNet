namespace LittleByte.MessageQueue.Serialization;

public interface IMessageSerializer
{
    ReadOnlyMemory<byte> Serialize(object message);
}
