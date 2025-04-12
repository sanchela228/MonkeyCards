using System.Drawing;
using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace MonkeyCards;

public abstract class Card
{
    public string Name { get; }
    public string ShortName { get; }

    public Rectangle _rect;
    public RenderTexture2D _canvas;
    
    private bool _isHovered = false;
    private bool _isDragging = false;
    private Vector2 _dragOffset = Vector2.Zero;
    private Vector2 _originalPosition = Vector2.Zero;

    private Rectangle _rectPlaceholder;
    
    protected Card(string name, string shortName)
    {
        #region BuildViewTexture
             Font customFont = Raylib.LoadFontEx(
                "Resources/Fonts/JockeyOne-Regular.ttf", 
                52,
                null, 
                0
            );
            
            Texture2D icon = Raylib.LoadTexture("Resources/Images/Icons/icon-hearts.png"); 
            Name = name;
            ShortName = shortName;

             _canvas = Raylib.LoadRenderTexture(205, 305);
             _rect = new Rectangle(
                 5,
                 5,
                 _canvas.Texture.Width - 5,
                 _canvas.Texture.Height - 10
             );
             
             Raylib.BeginTextureMode(_canvas);
             Raylib.ClearBackground(Color.Blank);
             
             Raylib.DrawRectangleRounded(new Rectangle(
                 0, 5,
                 _canvas.Texture.Width,
                 _canvas.Texture.Height - 5
                 ), 0.2f, 10, new Color(0, 0, 0, 128));
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

        #endregion

        var indowWidth = Raylib.GetRenderWidth();
        var inHeight = Raylib.GetRenderHeight();
        
        Vector2 point = new Vector2(indowWidth - (indowWidth / 4), inHeight / 2);
        Vector2 sizeR = new Vector2(_canvas.Texture.Width, _canvas.Texture.Height);
        
        var newpose = new Vector2(point.X - sizeR.X / 2, point.Y - sizeR.Y / 2);
        _rectPlaceholder = new Rectangle(newpose, sizeR);
    }

    public void Update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        
        _isHovered = Raylib.CheckCollisionPointRec(mousePos, _rectPlaceholder);
        
        if (_isHovered)
        {
            Raylib.SetMouseCursor(_isDragging ? MouseCursor.ResizeAll : MouseCursor.PointingHand);
            
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                _isDragging = true;
                _dragOffset = new Vector2(mousePos.X - _rectPlaceholder.X, mousePos.Y - _rectPlaceholder.Y);
                _originalPosition = _rectPlaceholder.Position;
            }
            
        }
        else if (!_isDragging)
        {
            Raylib.SetMouseCursor(MouseCursor.Arrow);
        }
        
        if (_isDragging)
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                _rectPlaceholder.Position = new Vector2(mousePos.X - _dragOffset.X, mousePos.Y - _dragOffset.Y);
            }
            else
            {
                _isDragging = false;
                Raylib.SetMouseCursor(_isHovered ? MouseCursor.PointingHand : MouseCursor.Default);
            }
        }
    }

    public void Draw()
    {
        
        Raylib.DrawTexturePro(
            _canvas.Texture,
            new Rectangle(0, 0, _canvas.Texture.Width, -_canvas.Texture.Height),
            _rectPlaceholder,
            Vector2.Zero,
            0f,
            Color.White
        );
        
        // Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);
    }
}