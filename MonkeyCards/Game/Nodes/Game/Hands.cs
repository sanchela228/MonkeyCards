using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Nodes.Game.Models;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game;

public class Hands : Node
{
    protected virtual PointRendering PointRendering { get; set; } = PointRendering.Center;
    public Hands(Vector2 centerPoint, float maxWidth, IEnumerable<Card> cards = null)
    {
        this.AddChildrens(cards);

        this.Position = centerPoint;
        this.Size = new Vector2(maxWidth, 10);
    }
    
    public override void Update(float deltaTime)
    {
        // TODO: rewrite this
        var count = _childrens.Count();
        
        if (count == 0)
            return;

        var i = 0;
        
        foreach (var card in _childrens)
        {
            card.Position = Vector2.Lerp(card.Position, new Vector2((Position.X - 250) + i*80, Position.Y), 18f * deltaTime);

            i++;
        }
        
        // float minSpacing = 100f;
        // int cardsCount = _childrens.Count();
        //
        // float fanMaxOffsetY = 30f;
        // float fanMaxAngle = 15f;
        //
        // if (cardsCount > 6 && cardsCount <= 9)
        // {
        //     int extraCards = cardsCount - 6;
        //     minSpacing = Math.Max(100f - extraCards * 15f, 10f);
        // }
        //
        // float totalWidth = (cardsCount - 1) * minSpacing;
        //
        // if (totalWidth > Size.X)
        // {
        //     minSpacing = Size.X / (cardsCount - 1);
        //     totalWidth = Size.X;
        // }
        //
        // float startX = Position.X - totalWidth / 2;
        //
        // int centerIndex1 = cardsCount / 2 - (cardsCount % 2 == 0 ? 1 : 0);
        // int centerIndex2 = cardsCount / 2;
        //
        // int i = 0;
        // foreach (var card in _childrens)
        // {
        //     float x = startX + i * minSpacing;
        //     float y = Position.Y;
        //     float rotation = 0f;
        //
        //     if (cardsCount > 4)
        //     {
        //         int distanceToCenter = Math.Min(
        //             Math.Abs(i - centerIndex1),
        //             cardsCount % 2 == 0 ? Math.Abs(i - centerIndex2) : int.MaxValue
        //         );
        //
        //         float normalizedDistance = (float)distanceToCenter / (cardsCount / 2);
        //
        //         y += normalizedDistance * fanMaxOffsetY;
        //         rotation = normalizedDistance * fanMaxAngle * (i < centerIndex1 ? -1 : 1);
        //
        //         if (cardsCount % 2 == 0 && (i == centerIndex1 || i == centerIndex2))
        //         {
        //             y += fanMaxOffsetY * 0.2f;
        //         }
        //     }
        //
        //     card.Position = new Vector2(x, y);
        //     card.Rotation = rotation * (MathF.PI / 180f);
        //     card.Size = new Vector2(205, 305);
        //
        //     i++;
        // }
    }
    
    public override void Draw()
    {
        Raylib.DrawRectangle(
            (int)Bounds.X,
            (int)Bounds.Y,
            (int)Bounds.Width,
            (int)Bounds.Height,
            Color.Green
        );
    }

    public override void Dispose()
    {
        
    }
}