using System.Numerics;
using SceneManager = MonkeyCards.Engine.Core.Scenes.Manager;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Controllers;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace MonkeyCards.Game.Nodes.Game.Models.Card;
using Engine.Core.Objects;

public class Card : Node
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Char Symbol { get; set; }
    public CardSuit Suit { get; set; }
    public float Cost { get; set; }
    public int Multiply { get; set; }
    public FontFamily FontFamily { get; set; }
    public View View { get; set; }
    public Border Border { get; set; }
    public Effect? Effect { get; set; }

    public string ShortName
    {
        get
        {
            return Symbol.ToString();
        }
    }

    protected RenderTexture2D _canvas;

    protected Font _font;
    protected Texture2D _icon;
    
    protected Hands _hands { get; set; }

    protected void LoadTmpTest()
    {
        _icon = Resources.Instance.Texture("Icons/icon-diamond.png");
        _font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42);
    }
    
    public Vector2 DefaultSize => new(136f, 206f);
    
    public Card(string _name, Hands Hands)
    {
        this.LoadTmpTest(); // TODO: replace this in resources manager
        
        Name = _name;
        Symbol = 'A';
        Size = this.DefaultSize;

        _hands = Hands;
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
    
    protected bool _isDragging = false;
    protected Vector2 _dragOffset;

    public Node ExParent;

    protected void BackToHands()
    {
        SetParent(_hands, DraggingCard.Instance.IndexCardOnHands ?? -1);
    }
    
    public override void Update(float deltaTime)
    {
        // TODO: change cursor view logic
        
        #region DraggingCard

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
                    DraggingCard.Instance.Card = null;
                    _isDragging = false;
                    
                    Vector2 worldPosition = Position;
                    
                    if (Parent is null) BackToHands();
                    
                    Position = worldPosition;
                    
                    DraggingCard.Instance.IndexCardOnHands = 0;
                    SceneManager.Instance.PeekScene().RemoveNode(this);
                    MouseTracking.Instance.BlockedHover = false;
                }
            }
            
            if (IsMouseOver() || _isDragging)
            {
                targetSize = new Vector2(1.2f, 1.2f);
                Order = 200;

                if (IsMousePressed())
                {
                    DraggingCard.Instance.Card = this;
                    DraggingCard.Instance.IndexCardOnHands = _hands.Childrens.IndexOf(this);
                    
                    _isDragging = true;
                    _dragOffset = new Vector2(mousePos.X - Position.X, mousePos.Y - Position.Y);
                    
                    MouseTracking.Instance.BlockedHover = true;
                    
                    Vector2 worldPosition = Position;
                    
                    ExParent = Parent;
                    this.SetParent( SceneManager.Instance.PeekScene() );
                    
                    Position = worldPosition;
                }
            }
            else Order = 100;

        #endregion
        
        float t = 1.0f - MathF.Exp(-18f * deltaTime);
        Scale = Vector2.Lerp(Scale, targetSize, t);
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
        if (Parent is not null)
        {
            Parent.Childrens.Remove(this);
            SetParent(null);
        }
        else if (SceneManager.Instance.PeekScene() is not null && SceneManager.Instance.PeekScene().ContainsNode(this))
            SceneManager.Instance.PeekScene().RemoveNode(this);
        
        if (DraggingCard.Instance.Card == this)
            DraggingCard.Instance.Card = null;
        
        MouseTracking.Instance.BlockedHover = false;
        MouseTracking.Instance.HoveredNode = null;
            
        Childrens.Clear();
    }
}