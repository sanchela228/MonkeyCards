using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table.Combo;

public class Line : Node
{
    public Font Font { get; }
    
    public Line()
    {
        Font = Raylib.LoadFontEx(
            "Resources/Fonts/JockeyOne-Regular.ttf", 
            42,
            null, 
            0
        );
    }
    public override void Update(float deltaTime)
    {
        if (Childrens.Any())
            Visuals.Render.PlaceInLine( Childrens, (int) Childrens[0].Bounds.Width, Position, 20 );
    }

    public override void Draw()
    {
        
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}