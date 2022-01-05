using System.Text.Json;

namespace LittleByte.Core.Json
{
    public class JsonNetService : IJsonService
    {
        public string Serialize<T>(T value) => JsonSerializer.Serialize(value);
        public T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json);
    }
}
