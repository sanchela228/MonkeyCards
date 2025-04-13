using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Nodes.Game.Models;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game;

public class Hands : Node
{
    public Hands(Vector2 centerPoint, float maxWidth, IEnumerable<Card> cards = null)
    {
        this.AddChildrens(cards);

        this.Position = centerPoint;
        this.Size = new Vector2(maxWidth, 1);
    }
    
    public override void Update(float deltaTime)
    {
        
    }
    
    public override void Draw()
    {
        Raylib.DrawRectanglePro(
            Bounds, 
            new Vector2(0, 0), 
            0, 
            Color.White
        );
    }

    public override void Dispose()
    {
        
    }
}