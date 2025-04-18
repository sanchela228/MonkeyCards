using System.Text.Json;
using System.Text.Json.Serialization;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace MonkeyCards.Game.Helpers;

public class FontFamilyConverter : JsonConverter<FontFamily>
{
    public override FontFamily Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            string? colorStr = root.GetProperty("Color").GetString();

            var parts = colorStr.Split(',');
            Color color = Color.Black;
            
            if (parts.Length == 4 && 
                byte.TryParse(parts[0], out byte r) &&
                byte.TryParse(parts[1], out byte g) &&
                byte.TryParse(parts[2], out byte b) &&
                byte.TryParse(parts[3], out byte a))
            {
                color = new Color(r, g, b, a);
            }
            
            var family = new FontFamily();
            family.Color = color;
            family.Size = root.GetProperty("Size").GetInt32();
            family.Font = Resources.Instance.FontEx( root.GetProperty("Font").GetString(), family.Size );

            return family;
        }
    }

    public override void Write(Utf8JsonWriter writer, FontFamily value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}