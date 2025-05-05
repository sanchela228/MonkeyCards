using System.Numerics;
using Engine.Core;
using Raylib_cs;

namespace Engine.Helpers;

public enum TextAlignment
{
    Left,
    Center,
    Right
}

public static class Text
{
    public static void DrawWrapped(FontFamily fontFamily, string text, Vector2 pos, float size, TextAlignment alignment = TextAlignment.Left)
    {
        string[] words = text.Split(' ');
        string currentLine = "";
        float y = pos.Y;

        foreach (string word in words)
        {
            string testLine = currentLine.Length > 0 ? currentLine + " " + word : word;
            float textWidth = Raylib.MeasureTextEx(fontFamily.Font, testLine, fontFamily.Size, fontFamily.Spacing).X;
        
            if (textWidth <= size) currentLine = testLine;
            else
            {
                if (currentLine.Length > 0)
                {
                    DrawAlignedText(fontFamily, currentLine, pos.X, y, size, alignment);
                    y += fontFamily.Size + fontFamily.Spacing;
                }
                currentLine = word;
            }
        }

        if (currentLine.Length > 0)
            DrawAlignedText(fontFamily, currentLine, pos.X, y, size, alignment);
    }

    public static void DrawPro(FontFamily fontFamily, string text, Vector2 pos, Vector2? origin = null, float? rotation = null)
    {
        var originLocal = fontFamily.CalcTextSize(text);
        
        Raylib.DrawTextPro(
            fontFamily.Font,
            text,
            pos,
            origin ?? new Vector2(originLocal.X / 2, originLocal.Y / 2),
            rotation ?? fontFamily.Rotation,
            fontFamily.Size,
            fontFamily.Spacing,
            fontFamily.Color
        );
    }

    private static void DrawAlignedText(FontFamily fontFamily, string text, float x, float y, float maxWidth, TextAlignment alignment)
    {
        float textWidth = Raylib.MeasureTextEx(fontFamily.Font, text, fontFamily.Size, fontFamily.Spacing).X;
        float startX = x;

        switch (alignment)
        {
            case TextAlignment.Center:
                startX = x + (maxWidth - textWidth) / 2;
                break;
            case TextAlignment.Right:
                startX = x + maxWidth - textWidth;
                break;
        }

        Raylib.DrawTextEx(
            fontFamily.Font,
            text,
            new Vector2(startX, y),
            fontFamily.Size,
            fontFamily.Spacing,
            fontFamily.Color
        );
    }
}