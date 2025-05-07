using System.Text.Json;
using System.Text.Json.Serialization;
using Game.Nodes.Game.Models.Card;

namespace Game.Helpers.Converters;

public class SpecialConverter : JsonConverter<Special>
{
    public override Special Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            string className = root.GetProperty("Type").GetString();
            
            Type specialType = Type.GetType($"Game.Nodes.Game.Models.Card.Specials.{className}") 
                               ?? throw new JsonException($"Unknown Special class: {className}");

            return (Special) JsonSerializer.Deserialize(root.GetRawText(), specialType, options);
        }
    }

    public override void Write(Utf8JsonWriter writer, Special value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}