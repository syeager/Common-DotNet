namespace LittleByte.Core.Json
{
    public interface IJsonService
    {
        string Serialize<T>(T value);
        T? Deserialize<T>(string json);
    }
}
