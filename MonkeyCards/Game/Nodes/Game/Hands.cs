using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Controllers;
using MonkeyCards.Game.Nodes.Game.Models;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game;

public class Hands : Node
{
    protected virtual PointRendering PointRendering { get; set; } = PointRendering.Center;
    public Hands(Vector2 centerPoint, float maxWidth, IEnumerable<Card> cards = null)
    {
        AddChildrens(cards);

        Position = centerPoint;
        Size = new Vector2(maxWidth, 260);
    }
    
    public override void Update(float deltaTime)
    {
        if ( Childrens.Any() )
        {
            var count = Childrens.Count;
            var cardSize = (int) ((Card) Childrens[0]).DefaultSize.X;
            int margin = -30;
            
            int countMargins = count - 1;
            if (count > 6) margin -= count * 5;
            
            int totalWidth = (cardSize * count + countMargins * margin);
            
            Vector2[] positions = new Vector2[count];
            
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector2(
                    (Position.X + ((cardSize + margin) * i)) - (totalWidth / 2) + cardSize / 2,
                    Position.Y
                );
            }
            
            float spreadAmount = 0f;
            int insertIndex = -1;

            if (DraggingCard.Instance.Card is Card && IsMouseOverWithoutOverlap())
            {
                Vector2 mousePos = Raylib.GetMousePosition();

                int closestIndex = 0;
                float minDistance = Vector2.Distance(mousePos, positions[0]);
                bool isMouseOnRight = mousePos.X > positions[0].X;

                for (int i = 1; i < positions.Length; i++)
                {
                    float currentDistance = Vector2.Distance(mousePos, positions[i]);
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        closestIndex = i;
                        isMouseOnRight = mousePos.X > positions[i].X;
                    }
                }
                
                DraggingCard.Instance.IndexCardOnHands = closestIndex + (isMouseOnRight ? 1 : 0);

                insertIndex = isMouseOnRight ? closestIndex + 1 : closestIndex;
                spreadAmount = cardSize * 0.5f;
            }

            for (int i = 0; i < positions.Length; i++)
            {
                float t = 1.0f - MathF.Exp(-18f * deltaTime);
                Vector2 targetPosition = positions[i];
    
                if (spreadAmount > 0 && insertIndex >= 0)
                {
                    if (i < insertIndex)
                        targetPosition.X -= spreadAmount;
                    else if (i >= insertIndex)
                        targetPosition.X += spreadAmount;
                }
    
                Childrens[i].Position = Vector2.Lerp(Childrens[i].Position, targetPosition, t);
            }
        }
    }
    
    public override void Draw()
    {
        // Raylib.DrawRectangle(
        //     (int)Bounds.X, (int)Bounds.Y,
        //     (int)Bounds.Width, (int)Bounds.Height,
        //     Color.Red
        // );
    }

    public override void Dispose()
    {
        
    }
}