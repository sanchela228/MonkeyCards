using System.Numerics;
using Raylib_cs;
namespace Game.Nodes.Game.Models.Card;

public struct FontFamily
{
    public Color Color { get; set; }
    public Font Font { get; set; }
    public int Size { get; set; }
    public float Rotation { get; set; }
    public float Spacing { get; set; }

    public Vector2 calcTextSize(string text) => Raylib.MeasureTextEx(Font, text, Spacing, Spacing);
}