using System.Numerics;
using Engine.Core;
using Engine.Core.Scenes;
using Engine.Helpers;
using Engine.Managers;
using Raylib_cs;
using Rectangle = Raylib_cs.Rectangle;

namespace Game.Scenes;

public class StartUp : Scene
{

    private FontFamily f;
    private FontFamily fm;
    private Texture2D t;
    private Vector2 centerScreen;
    
    private const string previewText = "We present a game in which there is a lot of blood, guts of death, and generally full of shit, yes.";
    private const float previewTextSize = 505f;
    
    public StartUp()
    {
        t = Resources.Instance.Texture("Images/MonkeyPreview.png");
        f = new FontFamily()
        {
            Font = Resources.Instance.FontEx("JandaManateeSolidCyrillic.ttf", 58),
            Size = 58,
            Spacing = 6,
            Color = Color.White
        };
        
        fm = new FontFamily()
        {
            Font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 34),
            Size = 34,
            Spacing = 2,
            Color = Color.White
        };
    }
    
    public override void Update(float deltaTime)
    {
        centerScreen = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Gray);

        Texture.DrawEx(t, new Vector2(centerScreen.X, centerScreen.Y - 111));
        Text.DrawPro(f, "MonkeySpeak", centerScreen);

        Text.DrawWrapped(fm, previewText, new Vector2(centerScreen.X - previewTextSize / 2, centerScreen.Y + 50), previewTextSize, TextAlignment.Center);
    }

    public override void Dispose()
    {
        
    }
}