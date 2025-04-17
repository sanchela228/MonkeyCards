using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Models.Card;

public struct View
{
    public TypeView Type { get; set; }
    public Texture2D? Texture { get; set; }
    public Vector2 Position
    {
        get => Vector2.Clamp(Position, Vector2.Zero, Vector2.One);
        set => Position = Vector2.Clamp(value, Vector2.Zero, Vector2.One);
    }
    public bool Sides { get; set; }
    public Color Color { get; set; }
}