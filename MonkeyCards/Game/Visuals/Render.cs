using System.Numerics;
using MonkeyCards.Engine.Core.Objects;

namespace MonkeyCards.Game.Visuals;

public static class Render
{
    public static void PlaceInLine(IEnumerable<Node> nodes, int pixelSizeElement, Vector2 centerPoint, int pixelsMargin, float lerp = 0f, float deltaTime = 0f)
    {
        var count = nodes.Count();
        var nodes2 = nodes.ToList();
        
        int countMargins = count - 1;
        int totalWidth = (pixelSizeElement * count + countMargins * pixelsMargin);
            
        for (int i = 0; i < count; i++)
        {
            Vector2 targetPosition = new Vector2(
                (centerPoint.X + ((pixelSizeElement + pixelsMargin) * i)) - (totalWidth / 2) + pixelSizeElement / 2, 
                centerPoint.Y
            );
            
            if (lerp > 0 && deltaTime > 0)
            {
                float t = 1.0f - MathF.Exp(-18f * deltaTime);
                nodes2[i].Position = Vector2.Lerp(nodes2[i].Position, targetPosition, t);
            }
            else nodes2[i].Position = targetPosition;
        }
    }
}