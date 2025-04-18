using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Models.Card;

public struct View
{
    public TypeView Type { get; set; }
    public Texture2D? Texture { get; set; }
    
    private Vector2 _position;
    public Vector2 Position
    {
        get => Vector2.Clamp(_position, Vector2.Zero, Vector2.One);
        set => _position = Vector2.Clamp(value, Vector2.Zero, Vector2.One);
    }
    public bool Sides { get; set; }
    public Color Color { get; set; }
}