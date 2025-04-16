using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table.Combo;

public class Element : Node
{
    protected Value _value;
    public Value Value { get; }
    
    protected Font _font;
    
    public Element(Value value, Font font)
    {
        _value = value;
        _font = font;
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw()
    {
        Raylib.DrawTextPro( 
            _font, 
            _value.Symbol.ToString(), 
            new Vector2(Position.X, Position.Y),
            new Vector2(21, 21),
            0f,
            42f,
            3f,
            Color.Red
        );
        
        // Raylib.DrawRectangle((int) Bounds.X, (int) Bounds.Y, 10, 10, Color.Red);
    }

    public override void Dispose()
    {
        
    }
}