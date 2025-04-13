using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Engine.Helpers;

public static class Rectangle
{
    public static Vector2 CenterShift(Vector2 centerPos, Vector2 size)
    {
        return new Vector2(centerPos.X - size.X / 2, centerPos.Y - size.Y / 2);
    }
    
    public static Raylib_cs.Rectangle CenterShiftRec(Vector2 centerPos, Vector2 size)
    {
        return new Raylib_cs.Rectangle(
            new Vector2(centerPos.X - size.X / 2, centerPos.Y - size.Y / 2),
            size
        );
    }
    
    public static Raylib_cs.Rectangle CenterShiftRec(Raylib_cs.Rectangle rect)
    {
        return new Raylib_cs.Rectangle(
            new Vector2(rect.Position.X - rect.Size.X / 2, rect.Position.Y - rect.Size.Y / 2), 
            rect.Size
        );
    }
}