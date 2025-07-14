using System.Numerics;
using Engine.Core;
using Engine.Helpers;
using Engine.Managers;
using Raylib_cs;
using Rectangle = Raylib_cs.Rectangle;

namespace Game.Nodes.Game.Models.Card;

public class Hint(string name, string desc, float cost)
{
    private FontFamily _fontFamilyTitle = new()
    {
        Color = Color.White,
        Font = Resources.Instance.Get<Font>("JockeyOne-Regular.ttf"),
        Rotation = 0f,
        Size = 34,
        Spacing = 2f
    };
    private FontFamily _fontFamilyDesc = new()
    {
        Color = Color.Gray,
        Font = Resources.Instance.Get<Font>("JockeyOne-Regular.ttf"),
        Rotation = 0f,
        Size = 30,
        Spacing = 2f
            
    };
    private FontFamily _fontFamilyCost = new()
    {
        Color = new Color() {R = 40, G = 235, B = 50, A = 255},
        Font = Resources.Instance.Get<Font>("JockeyOne-Regular.ttf"),
        Rotation = 0f,
        Size = 42,
        Spacing = 2f
    };

    private const float _overlayWidth = 260f;
    private const float _overlayHeight = 40f;
    private const float _margin = 10f;


    public void Draw(Rectangle bounds)
    {
        float textHeight = Text.CalculateWrappedTextHeight(
            _fontFamilyDesc, 
            desc,
            _overlayWidth - 20f
        );
        
        float centerX = bounds.X + (bounds.Width - _overlayWidth) / 2f;
        float topY = bounds.Y - _margin - _overlayHeight - textHeight - 35f;
        float overlayHeight = _overlayHeight + textHeight + 35f;
        
        if (topY < 0)
            topY += _margin + bounds.Height + _overlayHeight + 10f + textHeight + 35f;
        
        
        float textCenterX = centerX + _overlayWidth / 2f;
        
        Raylib.DrawRectangleRounded(
            new Rectangle(centerX, topY, _overlayWidth, overlayHeight), 
            0.2f, 
            10,
            new Color(25, 25, 25, 255)
        );
        
        Raylib.DrawRectangleRoundedLinesEx(
            new Rectangle(centerX, topY, _overlayWidth, overlayHeight), 
            0.2f, 
            10,
            4,
            new Color() {R = 55, G = 55, B = 55, A = 255}
        );
        
        Text.DrawPro(
            _fontFamilyTitle, 
            name, 
            new Vector2(textCenterX, topY + 20f)
        );
        
        Text.DrawPro(
            _fontFamilyCost, 
            cost.ToString() + "$", 
            new Vector2(textCenterX, topY + 50f)
        );
        
        Text.DrawWrapped(
            _fontFamilyDesc, 
            desc, 
            new Vector2(textCenterX - (_overlayWidth - 20f) / 2f, topY + 65f), 
            _overlayWidth - 20f, 
            TextAlignment.Center
        );
        
    }
}