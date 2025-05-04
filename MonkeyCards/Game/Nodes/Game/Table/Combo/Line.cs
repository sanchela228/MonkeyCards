using System.Numerics;
using Engine.Core.Objects;
using Engine.Managers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Nodes.Game.Table.Combo;

public class Line : Node
{
    public override void Update(float deltaTime)
    {
        if (Childrens.Any())
            Visuals.Render.PlaceInLine( Childrens, Position, 20 );
    }

    public override void Draw()
    {
        
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}