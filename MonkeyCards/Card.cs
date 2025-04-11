using System.Numerics;
using Raylib_cs;

namespace MonkeyCards;

public abstract class Card
{
    public string Name { get; }
    public string ShortName { get; }

    public Rectangle _rect;
    public RenderTexture2D _canvas;
    
    protected Card(string name, string shortName)
    {
        // Font customFont = Raylib.LoadFont("Resources/Fonts/JockeyOne-Regular.ttf");
        
        Font customFont = Raylib.LoadFontEx(
            "Resources/Fonts/JockeyOne-Regular.ttf", 
            52,
            null, 
            0
        );
        
        Texture2D icon = Raylib.LoadTexture("Resources/Images/Icons/icon-hearts.png"); 
        Name = name;
        ShortName = shortName;
         
         _canvas = Raylib.LoadRenderTexture(200, 300);
         
         _rect = new Rectangle(
             0,
             0,
             _canvas.Texture.Width,
             _canvas.Texture.Height
         );
         
         Raylib.BeginTextureMode(_canvas);
         Raylib.ClearBackground(Color.Blank);
        
         Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);
         
         Raylib.DrawTextPro( 
             customFont, 
             ShortName, 
             new Vector2(32, 22),
             new Vector2(16, 16),
             0f,
             52,
             3,
             Color.Black
         );
         
         Raylib.DrawTextPro( 
             customFont, 
            ShortName, 
            new Vector2(_canvas.Texture.Width - 32, _canvas.Texture.Height - 22),
            new Vector2(16, 16),
            180f,
            52,
            3,
            Color.Black
         );
         
         Rectangle iconDest = new Rectangle(
             _canvas.Texture.Width - 28,
             _canvas.Texture.Height - 72,
             40, 
             40
         );
         
         Raylib.DrawTexturePro(
             icon,
             new Rectangle(0, 0, icon.Width, icon.Height), 
             iconDest,
             new Vector2(20, 20),
             180f,
             Color.White
         );
        
         
        
         
         Rectangle iconDestThree = new Rectangle(
             (_canvas.Texture.Width / 2),
             (_canvas.Texture.Height / 2),
             150, 
             150
         );
         
         Raylib.DrawTexturePro(
             icon,
             new Rectangle(0, 0, icon.Width, icon.Height),
             iconDestThree,
             new Vector2(75, 75),
             0.0f,
             Color.White
         );
        
        
         
         Rectangle iconDestTwo = new Rectangle(
             28,
             72,
             40, 
             40
         );
         
         Raylib.DrawTexturePro(
             icon,
             new Rectangle(0, 0, icon.Width, icon.Height),
             iconDestTwo,
             new Vector2(20, 20),
             0.0f,
             Color.White
         );
         
         Raylib.EndTextureMode();
    }

    public void Draw()
    {

        Raylib.DrawTexturePro(
            _canvas.Texture,
            new Rectangle(0, 0, _canvas.Texture.Width, -_canvas.Texture.Height),
            new Rectangle(400, 80, _canvas.Texture.Width, _canvas.Texture.Height),
            new Vector2(),
            0f,
            Color.White
        );
        
        
        // Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);

        
    }
}