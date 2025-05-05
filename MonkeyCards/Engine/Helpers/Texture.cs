using System.Numerics;
using Raylib_cs;

namespace Engine.Helpers;


public enum PointRendering
{
    LeftTop,
    Center,
}

public static class Texture
{
    public static void DrawEx(Texture2D t, Vector2 pos, float rotation = 0, float scale = 1f, Color? c = null, PointRendering pointRendering = PointRendering.Center )
    {
        if (pointRendering == PointRendering.Center)
            pos = new Vector2(pos.X - t.Width / 2, pos.Y - t.Height / 2);
        
        Raylib.DrawTextureEx(
            t, 
            pos, 
            rotation,
            scale, 
            c ?? Color.White
        );
    }
}