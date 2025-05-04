using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Engine.Managers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Helpers.Converters;

public class ViewConverter : JsonConverter<View>
{
    public override View Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            var view = new View();
            
            view.Sides = JsonSerializer.Deserialize<bool>(root.GetProperty("Sides").GetRawText());
            
            #region PositionRead

            if (root.TryGetProperty("Position", out var pos))
            {
                if (pos.ValueKind == JsonValueKind.String)
                {
                    string? positionStr = pos.GetString();
                    if (positionStr != null)
                    {
                        var parts = positionStr.Split(',');
                        if (parts.Length == 2 && 
                            float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && 
                            float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
                        {
                            view.AddPosition(new Vector2(x, y));
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
                                    float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && 
                                    float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
                                {
                                    view.AddPosition(new Vector2(x, y));
                                }
                            }
                        }
                    }
                }
            }
            
            #endregion

            #region SizeRead
            if (root.TryGetProperty("Size", out var size))
            {
                if (size.ValueKind == JsonValueKind.String)
                {
                    string? sizeStr = size.GetString();
                    if (sizeStr != null)
                    {
                        var parts = sizeStr.Split(',');
                        if (parts.Length == 2 && 
                            float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && 
                            float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
                        {
                            view.Size.Add(new Vector2(x, y));
                        }
                    }
                }
                else if (size.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in size.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.String)
                        {
                            string? sizeStr = item.GetString();
                            var parts = sizeStr.Split(',');
                            if (parts.Length == 2 && 
                                float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && 
                                float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
                            {
                                view.Size.Add(new Vector2(x, y));
                            }
                        }
                    }
                }
            }
            #endregion

            if (root.TryGetProperty("Rotation", out var rotation))
            {
                if (rotation.ValueKind == JsonValueKind.Number)
                {
                    int? rt = rotation.GetInt32();
                    view.Rotate.Add(rt.Value);
                }
                else if (rotation.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in rotation.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Number)
                        {
                            int? rt = item.GetInt32();
                            view.Rotate.Add(rt.Value);
                        }
                    }
                }
            }
            
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