using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Controllers;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game;

public class SessionPlayerStatus : Node
{
    private Font _font;
    public SessionPlayerStatus(Vector2 pos)
    {
        Position = pos;
        _font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42);
    }
    
    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw()
    {
        Raylib.DrawTextPro( 
            _font, 
            Session.Instance.Self.Money + "$", 
            new Vector2(Position.X, Position.Y),
            new Vector2(21, 21),
            0f,
            42,
            3,
            Color.Black
        );
    }

    public override void Dispose()
    {
        
    }
}