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

            if (root.TryGetProperty("Position", out var pos))
            {
                if (pos.ValueKind == JsonValueKind.String)
                {
                    string? positionStr = root.GetProperty("Position").GetString();
                    
                    if (positionStr != null)
                    {
                        var parts = positionStr.Split(',');
                        if (parts.Length == 2 && 
                            float.TryParse(parts[0], out float x) && 
                            float.TryParse(parts[1], out float y))
                        {
                            view.AddPosition( new Vector2(x, y) );
                        }
                    }
                }
                else if (pos.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in pos.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.String)
                        {
                            string? positionStr = item.GetString();
                            
                            if (positionStr != null)
                            {
                                var parts = positionStr.Split(',');
                                if (parts.Length == 2 && 
                                    float.TryParse(parts[0], out float x) && 
                                    float.TryParse(parts[1], out float y))
                                {
                                    view.AddPosition( new Vector2(x, y) );
                                }
                            }
                        }
                    }
                }
            }
            
            #endregion

            view.Texture = Resources.Instance.Texture(root.GetProperty("Texture").GetString());

            #region ColorRead

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

            #endregion
            
            return view;
        }
    }

    public override void Write(Utf8JsonWriter writer, View value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}