using System.Numerics;
using MonkeyCards.Game.Nodes.Game.Models;

namespace MonkeyCards.Game.Controllers;

public class Hands
{ 
    public void PlaceCards(Vector2 centerPoint, float maxWidth, IEnumerable<Card> cards)
    {
        if (cards.Count() == 0)
            return;

        float minSpacing = 100f;
        int cardsCount = cards.Count();

        float fanMaxOffsetY = 30f;
        float fanMaxAngle = 15f;

        if (cardsCount > 6 && cardsCount <= 9)
        {
            int extraCards = cardsCount - 6;
            minSpacing = Math.Max(100f - extraCards * 15f, 10f);
        }

        float totalWidth = (cardsCount - 1) * minSpacing;

        if (totalWidth > maxWidth)
        {
            minSpacing = maxWidth / (cardsCount - 1);
            totalWidth = maxWidth;
        }

        float startX = centerPoint.X - totalWidth / 2;

        int centerIndex1 = cardsCount / 2 - (cardsCount % 2 == 0 ? 1 : 0);
        int centerIndex2 = cardsCount / 2;

        int i = 0;
        foreach (var card in cards)
        {
            float x = startX + i * minSpacing;
            float y = centerPoint.Y;
            float rotation = 0f;

            if (cardsCount > 4)
            {
                int distanceToCenter = Math.Min(
                    Math.Abs(i - centerIndex1),
                    cardsCount % 2 == 0 ? Math.Abs(i - centerIndex2) : int.MaxValue
                );

                float normalizedDistance = (float)distanceToCenter / (cardsCount / 2);

                y += normalizedDistance * fanMaxOffsetY;
                rotation = normalizedDistance * fanMaxAngle * (i < centerIndex1 ? -1 : 1);

                if (cardsCount % 2 == 0 && (i == centerIndex1 || i == centerIndex2))
                {
                    y += fanMaxOffsetY * 0.2f;
                }
            }

            card.Position = new Vector2(x, y);
            card.Rotation = rotation * (MathF.PI / 180f);
            card.Size = new Vector2(205, 305);
            
            i++;
        }
    }
}