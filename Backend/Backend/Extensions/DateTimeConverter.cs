using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Backend.Extensions
{
    public class DateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            // ReSharper disable once StringLiteralTypo
            var dateStr = ((DateTime)value!).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
            JToken t = JToken.FromObject(dateStr);
            t.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(DateTime));
        }

        public override bool CanRead { get; } = false;
    }
}
