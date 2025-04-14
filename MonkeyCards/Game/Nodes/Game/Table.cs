using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table;

public class Table : Node
{
    public Table(Vector2 centerPoint)
    {
        Position = centerPoint;
        AddChildrens( new List<Placeholder>
        {
            new Placeholder(),
            new Placeholder(),
            new Placeholder(),
            new Placeholder(),
            new Placeholder()
        } );
        
        if ( Childrens.Any() )
        {
            var count = Childrens.Count;
            int cardSize = (int) Childrens[0].Size.X;
            int margin = 20;
            
            int countMargins = count - 1;
            int totalWidth = (cardSize * count + countMargins * margin);
            
            for (int i = 0; i < count; i++)
            {
                Childrens[i].Position = new Vector2(
                    (Position.X + ((cardSize + margin) * i)) - (totalWidth / 2) + cardSize / 2, 
                    centerPoint.Y
                );
            }
        }
    }
    
    public override void Update(float deltaTime)
    {
        
    }

    public override void Draw()
    {
        // Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 20, 20, Color.Black);
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}