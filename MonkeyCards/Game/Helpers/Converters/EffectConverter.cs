using System.Text.Json;
using System.Text.Json.Serialization;
using Game.Nodes.Game.Models.Card;

namespace Game.Helpers.Converters;


// TODO: REWRITE like special converter

public class EffectConverter : JsonConverter<Effect>
{
    public override Effect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            EffectType type = JsonSerializer.Deserialize<EffectType>(
                root.GetProperty("Type").GetRawText(),
                options
            );

            return type switch
            {
                EffectType.Shake => JsonSerializer.Deserialize<ShakeEffect>(root.GetRawText(), options),
                EffectType.Glow => JsonSerializer.Deserialize<GlowEffect>(root.GetRawText(), options),
                _ => null
            };
        }
    }

    public override void Write(Utf8JsonWriter writer, Effect value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}