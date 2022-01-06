namespace LittleByte.Messaging;

public interface IMessageSerializer
{
    ReadOnlyMemory<byte> Serialize(object message);
}
