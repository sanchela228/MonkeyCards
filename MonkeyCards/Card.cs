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
        Font customFont = Raylib.LoadFont("Resources/Fonts/JandaManateeSolidCyrillic.ttf");
        
        Name = name;
        ShortName = shortName;
        
        string[] suits = { "♠", "♥", "♦", "♣" };
         
         _canvas = Raylib.LoadRenderTexture(240, 400);
         
         _rect = new Rectangle(
             0,
             0,
             _canvas.Texture.Width,
             _canvas.Texture.Height
         );
         
         Raylib.BeginTextureMode(_canvas);
         Raylib.ClearBackground(Color.Blank);
        
         Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);
         
         Raylib.DrawText(this.ShortName, 
             20, 
             20, 
             32, 
             Color.Black
         );
         
         Raylib.DrawText(suits[0], 20, 80, 32, Color.Black);
         
         Raylib.DrawTextPro( 
             customFont, 
            ShortName, 
            new Vector2(_canvas.Texture.Width - 32, _canvas.Texture.Height - 32),
            new Vector2(16, 16),
            180f,
            52,
            3,
            Color.Black
         );
         
         Raylib.DrawTextPro( 
             customFont, 
             suits[0], 
             new Vector2(_canvas.Texture.Width - 32, _canvas.Texture.Height - 82),
             new Vector2(16, 16),
             180f,
             52,
             3,
             Color.Black
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