using System.Numerics;
using MonkeyCards.Engine.Managers;
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
    // protected OverlapsMode Overlap { get; set; } = OverlapsMode.None;
    // protected override PointRendering PointRendering { get; set; } = PointRendering.LeftTop;

    private void LoadTmpTest()
    {
        _font = Raylib.LoadFontEx(
            "Resources/Fonts/JockeyOne-Regular.ttf", 
            42,
            null, 
            0
        );
            
        _icon = Raylib.LoadTexture("Resources/Images/Icons/icon-diamond.png"); 
    }
    
    public Vector2 DefaultSize => new Vector2(136f, 206f);
    
    public Card(string _name, string shortName)
    {
        this.LoadTmpTest(); // TODO: replace this in resources manager
        
        Name = _name;
        ShortName = shortName;
        Size = this.DefaultSize;
        
        _canvas = Raylib.LoadRenderTexture((int) Size.X, (int) Size.Y);

        Rectangle placeholder = new Rectangle(
            5,
            0,
            _canvas.Texture.Width - 5,
            _canvas.Texture.Height - 5
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
                new Color() {R = 220, G = 220, B = 220, A = 255}
            );
            
            Raylib.DrawTextPro( 
                _font, 
                ShortName, 
                new Vector2(35, 22),
                new Vector2(16, 16),
                0f,
                42,
                3,
                Color.Black
            );
            
            Raylib.DrawTextPro( 
                _font, 
                ShortName, 
                new Vector2(placeholder.Width - 26, placeholder.Height - 14),
                new Vector2(16, 16),
                180f,
                42,
                3,
                Color.Black
            );
            
            Raylib.DrawTexturePro(
                _icon,
                new Rectangle(0, 0, _icon.Width, _icon.Height), 
                new Rectangle(
                    27, 
                    56, 
                    24, 
                    24
                ),
                new Vector2(12, 12),
                0f,
                Color.White
            );
             
            Raylib.DrawTexturePro(
                _icon,
                new Rectangle(0, 0, _icon.Width, _icon.Height), 
                new Rectangle(
                    _canvas.Texture.Width - 22, 
                    _canvas.Texture.Height - 56, 
                    24, 
                    24
                ),
                new Vector2(12, 12),
                180f,
                Color.White
            );
            
            Raylib.DrawTexturePro(
                _icon,
                new Rectangle(0, 0, _icon.Width, _icon.Height),
                new Rectangle(
                    _canvas.Texture.Width / 2 + 3,
                    _canvas.Texture.Height / 2,
                    80,
                    80
                ),
                new Vector2(40, 40),
                0.0f,
                Color.White
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
        
        if (_isDragging)
        {
            MouseTracking.Instance.HoveredNode = this;
            
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                Position = new Vector2(mousePos.X - _dragOffset.X, mousePos.Y - _dragOffset.Y);
            }
            else
            {
                _isDragging = false;
                MouseTracking.Instance.BlockedHover = false;
            }
        }
    
        if (IsMouseOver() || _isDragging)
        {
            Raylib.SetMouseCursor(MouseCursor.PointingHand);
            targetSize = new Vector2(1.2f, 1.2f);
            Order = 200;

            if (IsMousePressed())
            {
                _isDragging = true;
                _dragOffset = new Vector2(mousePos.X - Position.X, mousePos.Y - Position.Y);
                MouseTracking.Instance.BlockedHover = true;
                // this.SetParent(null);
            }
        }
        else
        {
            Order = 100;
            Raylib.SetMouseCursor(MouseCursor.Default);
        }
    
        float t = 1.0f - MathF.Exp(-18f * deltaTime);
        Scale = Vector2.Lerp(Scale, targetSize, t);
    }

    public override void Draw()
    {
        Console.WriteLine("CARD DRAW");
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