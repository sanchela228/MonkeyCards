using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace MonkeyCards.Game.Helpers;

public class ViewConverter : JsonConverter<View>
{
    public override View Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            var view = new View();
            
            view.Type = JsonSerializer.Deserialize<TypeView>(
                root.GetProperty("Type").GetRawText(),
                options
            );
            
            view.Sides = JsonSerializer.Deserialize<bool>(root.GetProperty("Sides").GetRawText());
            
            #region PositionRead
            string? positionStr = root.GetProperty("Position").GetString();

            Vector2 vec2 = new Vector2(0.5f, 0.5f);
            if (positionStr != null)
            {
                var parts = positionStr?.Split(',');
                
                if (parts is not null && parts.Length == 2 && 
                    byte.TryParse(parts[0], out byte x) &&
                    byte.TryParse(parts[1], out byte y) )
                {
                    vec2 = new Vector2(x, y);
                }
            }
            
            view.Position = vec2;
            #endregion

            view.Texture = Resources.Instance.Texture(root.GetProperty("Texture").GetString());
            
            string? colorStr = root.GetProperty("Color").GetString();
            
            var colorParts = colorStr.Split(',');
            Color color = Color.Black;
            
            if (colorParts.Length == 4 && 
                byte.TryParse(colorParts[0], out byte r) &&
                byte.TryParse(colorParts[1], out byte g) &&
                byte.TryParse(colorParts[2], out byte b) &&
                byte.TryParse(colorParts[3], out byte a))
            {
                color = new Color(r, g, b, a);
            }
            
            view.Color = color;
            
            return view;
        }
    }

    public override void Write(Utf8JsonWriter writer, View value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}