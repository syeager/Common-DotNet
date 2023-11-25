using System.Text;
using Serilog.Events;
using Serilog.Formatting;

namespace LittleByte.Common.Logging.Formatters
{
    public class PromtailFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            var properties = new Dictionary<string, string?>
            {
                ["level"] = logEvent.Level.ToString().ToLowerInvariant(),
                ["message_template"] = $"\"{TransformValue(logEvent.MessageTemplate.Text)}\"",
            };

            if(logEvent.Exception != null)
            {
                properties["exception_type"] = logEvent.Exception.GetType().FullName;
                properties["exception_message"] = TransformValue(logEvent.Exception.Message);
                properties["exception_stack_trace"] = TransformValue(logEvent.Exception.StackTrace);
            }

            foreach(var property in logEvent.Properties)
            {
                properties[property.Key] = TransformValue(property.Value);
            }

            var builder = new StringBuilder();
            foreach(var (key, value) in properties)
            {
                builder
                    .Append(key)
                    .Append('=')
                    .Append(value)
                    .Append(' ');
            }

            output.WriteLine(builder.ToString());
        }

        private static string? TransformValue(object? value) => value?.ToString()?.Replace("\r\n", "\n");
    }
}
