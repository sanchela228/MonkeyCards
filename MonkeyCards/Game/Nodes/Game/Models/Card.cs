using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Models;
using Engine.Core.Objects;

public class Card : Node
{
    public string Name;

    public Card(string _name)
    {
        Name = _name;
    }
    
    public override void Update(float deltaTime)
    {
        
    }


    public override void Draw()
    {
        // Console.WriteLine("Draw Card", Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
        
        
        Raylib.DrawRectangleRounded(
            Bounds, 
            0.2f, 
            10, 
            Color.White
        );
    }

    public override void Dispose()
    {
        
    }
}