using System.Drawing;
using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace MonkeyCards.Game.Nodes.Game.Models;
using Engine.Core.Objects;

public class Card : Node
{
    public string Name;
    public string ShortName { get; }

    private RenderTexture2D _canvas;

    private Font _font;
    private Texture2D _icon;
    

    // protected override PointRendering PointRendering { get; set; } = PointRendering.LeftTop;

    private void LoadTmpTest()
    {
        _font = Raylib.LoadFontEx(
            "Resources/Fonts/JockeyOne-Regular.ttf", 
            52,
            null, 
            0
        );
            
        _icon = Raylib.LoadTexture("Resources/Images/Icons/icon-hearts.png"); 
    }
    
    private Vector2 DefaultSize => new Vector2(136f, 206f);
    
    public Card(string _name, string shortName)
    {
        this.LoadTmpTest(); // TODO: replace this in resources manager
        
        Name = _name;
        ShortName = shortName;
        Size = this.DefaultSize;
        
        _canvas = Raylib.LoadRenderTexture((int) Size.X, (int) Size.Y);

        Rectangle placeholder = new Rectangle(
            5,
            5,
            _canvas.Texture.Width - 5,
            _canvas.Texture.Height - 10
        );

        #region SetupRenderTexture
        
            Raylib.BeginTextureMode(_canvas);
            
            Raylib.ClearBackground(Color.Blank);
            
            Raylib.DrawRectangleRounded(
                new Rectangle(
                    placeholder.X - 5, placeholder.Y,
                    placeholder.Width,
                    placeholder.Height + 5
                ), 
                0.2f, 
                10, 
                new Color(0, 0, 0, 128)
            );
            
            Raylib.DrawRectangleRounded(
                placeholder, 
                0.2f, 
                10, 
                Color.White
            );
            
            Raylib.DrawRectangleRoundedLinesEx(
                Engine.Helpers.Rectangle.ChangeProportionSize(placeholder, -3f), 
                0.2f, 
                10,
                4,
                new Color() {R = 0, G = 0, B = 0, A = 35}
            );
            
            
            Raylib.EndTextureMode();
        
        #endregion
        
        
    }
    
    private bool _isDragging = false;
    private Vector2 _dragOffset;
    public override void Update(float deltaTime)
    {
        var targetSize = Vector2.One;
        Vector2 mousePos = Raylib.GetMousePosition();
        
        if (IsMouseOver())
        {
            Raylib.SetMouseCursor(MouseCursor.PointingHand);
            targetSize = new Vector2(1.2f, 1.2f);

            if (IsMousePressed())
            {
                _isDragging = true;
                _dragOffset = new Vector2(mousePos.X - Position.X, mousePos.Y - Position.Y);
            }
        }
        else Raylib.SetMouseCursor(MouseCursor.Default);

        if (_isDragging)
        {
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                Position = new Vector2(mousePos.X - _dragOffset.X, mousePos.Y - _dragOffset.Y);
            }
            else
            {
                _isDragging = false;
                Raylib.SetMouseCursor(IsMouseOver() ? MouseCursor.PointingHand : MouseCursor.Default);
            }
        }
        
        Scale = Vector2.Lerp(Scale, targetSize, 18f * deltaTime);
    }

    public override void Draw()
    {
        Raylib.DrawTexturePro(
            _canvas.Texture,
            new Rectangle(0, 0, _canvas.Texture.Width, -_canvas.Texture.Height),
            new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height),
            Vector2.Zero,
            Rotation,
            Color.White
        );
    }

    public override void Dispose()
    {
        Raylib.UnloadFont(_font);
        Raylib.UnloadTexture(_icon);
    }
}