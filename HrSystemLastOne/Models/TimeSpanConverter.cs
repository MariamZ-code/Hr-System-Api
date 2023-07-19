using System.Text.Json.Serialization;
using System.Text.Json;

namespace HrSystemLastOne.Models
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }

        public void WriteValue(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("hours", value.Hours);
            writer.WriteNumber("Minutes", value.Minutes);
            /* insert any needed properties here */
            writer.WriteEndObject();
        }
    }
}
