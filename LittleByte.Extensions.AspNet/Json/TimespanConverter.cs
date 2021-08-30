using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LittleByte.Extensions.AspNet.Json
{
    public class TimespanConverter : JsonConverter<TimeSpan>
    {
        private const string DefaultFormat = @"d\.hh\:mm\:ss\:fff";

        private readonly string format;

        public TimespanConverter(string format = DefaultFormat)
        {
            this.format = format;
        }

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TimeSpan.TryParseExact(reader.GetString(), format, null, out var parsedTimeSpan);
            return parsedTimeSpan;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var timespanFormatted = $"{value.ToString(format, CultureInfo.InvariantCulture)}";
            writer.WriteStringValue(timespanFormatted);
        }
    }
}