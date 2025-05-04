using System.Numerics;
using Engine.Core.Objects;
using Engine.Managers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Nodes.Game.Table.Combo;

public class Element : Node
{
    protected Font _font;
    protected string _symbol;
    protected CardSuit _suit;
    
    protected Color color = Color.Black;
    
    public Element(string symbol, CardSuit suit)
    {
        _symbol = symbol;
        _suit = suit;
        _font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42);
        
        if (_suit == CardSuit.Hearts || _suit == CardSuit.Diamonds)
            color = Color.Red;
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw()
    {
        Raylib.DrawTextPro( 
            _font, 
            _symbol.ToString(), 
            new Vector2(Position.X, Position.Y),
            new Vector2(21, 21),
            0f,
            42f,
            3f,
            color
        );
    }

    public override void Dispose()
    {
        
    }
}