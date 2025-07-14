using System.Numerics;
using Engine.Core;
using Engine.Helpers;
using Engine.Managers;
using SceneManager = Engine.Core.Scenes.Manager;
using Game.Controllers;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace Game.Nodes.Game.Models.Card;
using Engine.Core.Objects;

public class Card : Node, ICloneable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Symbol { get; set; }

    private CardSuit _suit;
    public CardSuit Suit
    {
        get => _suit; 
        set
        {
            _suit = value;
            ReRenderTexture?.Invoke();
        }
    }
    
    public object Clone()
    {
        return MemberwiseClone();
    }

    public float Cost { get; set; }
    public int Multiply { get; set; } = 1;
    public Border Border { get; set; } = Border.Default;
    public BackgroundType Background { get; set; } = BackgroundType.Default;
    public FontFamily FontFamily { get; set; }
    public View View { get; set; }
    public Effect? Effect { get; set; }
    public Special? Special { get; set; }

    private bool _blocked;
    public void Block(bool b) => _blocked = b;
    public bool IsBlocked() => _blocked;

    public Highlight? Highlight;

    public string ShortName
    {
        get
        {
            var isNumber = false;
            var s = Symbol;
            
            if (s == "2" || s == "3")
                isNumber = true;
            
            if (isNumber && Multiply > 1)
                return Multiply + "x" + Symbol;
            
            if (!isNumber && Multiply == 2)
                return Symbol + Symbol;
                
            if (Multiply > 2)
                return Multiply + "x" + Symbol;
            
            return Symbol;
        }
    }

    protected RenderTexture2D _canvas;

    protected Font _font;
    protected Texture2D _icon;
    
    protected Hands? _hands { get; set; }
    public Vector2 DefaultSize => new(136f, 206f);
    
    public Card( Guid Id, string Name, string Symbol, CardSuit Suit, float Cost, FontFamily FontFamily, 
        View View, string Description = null, int multiply = 1, Border Border = Border.Default, 
        BackgroundType background = BackgroundType.Default, Effect? Effect = null, Special? Special = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Symbol = Symbol;
        this.Cost = Cost;
        this.Suit = Suit;
        this.FontFamily = FontFamily;
        this.View = View;
        this.Description = Description;
        this.Multiply = multiply;
        this.Border = Border;
        this.Background = background;
        this.Effect = Effect;
        this.Special = Special;
        
        PreventParent += node =>
        {
            if (node is not null && node is Hands)
                _hands = (Hands) node;
        };

        ReRenderTexture += SetupRenderTexture;
        
        Size = DefaultSize;

        // Collider = new Rectangle(-25, 0, this.DefaultSize.X - 50, this.DefaultSize.Y);
        _canvas = Raylib.LoadRenderTexture((int) Size.X, (int) Size.Y);

        SetupRenderTexture();
        
        Rectangle placeholder = new Rectangle(
            5,
            0,
            _canvas.Texture.Width - 5,
            _canvas.Texture.Height - 5
        );
        
        Effect?.Start(this, placeholder);
    }

    private event Action ReRenderTexture;
    
    private void SetupRenderTexture()
    {
        Rectangle placeholder = new Rectangle(
            5,
            0,
            _canvas.Texture.Width - 5,
            _canvas.Texture.Height - 5
        );
        
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
            View.Color
        );
        
        Raylib.DrawRectangleRoundedLinesEx(
            Engine.Helpers.Rectangle.ChangeProportionSize(placeholder, -3f), 
            0.2f, 
            10,
            4,
            new Color() {R = 220, G = 220, B = 220, A = 255}
        );
        
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
            View.Color
        );
        
        Raylib.DrawRectangleRoundedLinesEx(
            Engine.Helpers.Rectangle.ChangeProportionSize(placeholder, -3f), 
            0.2f, 
            10,
            4,
            new Color() {R = 220, G = 220, B = 220, A = 255}
        );
        
        Raylib.DrawTextPro(
            FontFamily.Font, 
            ShortName, 
            new Vector2(40, 24),
            new Vector2(FontFamily.Size / 2, FontFamily.Size / 2),
            0f,
            FontFamily.Size,
            3,
            FontFamily.Color
        );
        
        Raylib.DrawTextPro(
            FontFamily.Font,
            ShortName, 
            new Vector2(placeholder.Width - 30, placeholder.Height - 20),
            new Vector2(FontFamily.Size / 2, FontFamily.Size / 2),
            180f,
            FontFamily.Size,
            3,
            FontFamily.Color
        );

        if (View.Sides)
        {
            Raylib.DrawTexturePro(
                View.Texture,
                new Rectangle(0, 0, View.Texture.Width, View.Texture.Height), 
                new Rectangle(
                    25, 
                    52, 
                    20, 
                    20
                ),
                new Vector2(10, 10),
                0f,
                Color.White
            );
         
            Raylib.DrawTexturePro(
                View.Texture,
                new Rectangle(0, 0, View.Texture.Width, View.Texture.Height), 
                new Rectangle(
                    _canvas.Texture.Width - 21, 
                    _canvas.Texture.Height - 52, 
                    20, 
                    20
                ),
                new Vector2(12, 12),
                180f,
                Color.White
            );
        }

        for (var i = 0; i < View.Positions.Count; i++)
        {
            int sizePickerIndex = i;
            
            if (View.Size.Count < i + 1)
                sizePickerIndex = View.Size.Count - 1;
            
            Vector2 size = View.Size[sizePickerIndex];
            
            int rotatePickerIndex = i;
            
            if (View.Rotate.Count < i + 1)
                rotatePickerIndex = View.Rotate.Count - 1;
            
            float rotate = View.Rotate[rotatePickerIndex];
            
            Raylib.DrawTexturePro(
                View.Texture,
                new Rectangle(0, 0, View.Texture.Width, View.Texture.Height),
                new Rectangle(
                    _canvas.Texture.Width * View.Positions[i].X + 3,
                    _canvas.Texture.Height * View.Positions[i].Y,
                    size.X,
                    size.Y
                ),
                new Vector2(size.X / 2, size.Y / 2),
                rotate,
                Color.White
            );
        }
        
        for (var i = 0; i < View.Positions.Count; i++)
        {
            int sizePickerIndex = i;
            
            if (View.Size.Count < i + 1)
                sizePickerIndex = View.Size.Count - 1;
            
            Vector2 size = View.Size[sizePickerIndex];
            
            int rotatePickerIndex = i;
            
            if (View.Rotate.Count < i + 1)
                rotatePickerIndex = View.Rotate.Count - 1;
            
            float rotate = View.Rotate[rotatePickerIndex];
            
            Raylib.DrawTexturePro(
                View.Texture,
                new Rectangle(0, 0, View.Texture.Width, View.Texture.Height),
                new Rectangle(
                    _canvas.Texture.Width * View.Positions[i].X + 3,
                    _canvas.Texture.Height * View.Positions[i].Y,
                    size.X,
                    size.Y
                ),
                new Vector2(size.X / 2, size.Y / 2),
                rotate,
                Color.White
            );

        }
        
        if (View.ReversText)
        {
            var name = ShortName;

            if (this.Suit == CardSuit.Joker)
                name = "JOKER";
            
            Text.DrawWrappedWordBySymbols(this.FontFamily, name, new Vector2(24, 24));
            
            Text.DrawWrappedWordBySymbols(
                this.FontFamily, 
                name, 
                new Vector2(placeholder.Width - 15, placeholder.Height - 20), 
                reverse: true
            );
        }
        else
        {
            Raylib.DrawTextPro( 
                FontFamily.Font, 
                ShortName, 
                new Vector2(40, 24),
                new Vector2(FontFamily.Size / 2, FontFamily.Size / 2),
                0f,
                FontFamily.Size,
                3,
                FontFamily.Color
            );
        
            Raylib.DrawTextPro( 
                FontFamily.Font, 
                ShortName, 
                new Vector2(placeholder.Width - 30, placeholder.Height - 20),
                new Vector2(FontFamily.Size / 2, FontFamily.Size / 2),
                180f,
                FontFamily.Size,
                3,
                FontFamily.Color
            );
        }

        Raylib.EndTextureMode();
        
        Effect?.Start(this, placeholder);
    }
    
    protected bool _isDragging = false;
    protected bool _canInteract = true;
    protected Vector2 _dragOffset;

    public Node ExParent;
    
    private bool _canBackToHand = true;

    public void SetHand(Hands hands)
    {
        _hands = hands;
        ExParent = hands;
    }

    public void BackToHands()
    {
        if ( _hands is not null && _canBackToHand)
            SetParent(_hands, DraggingCard.Instance.IndexCardOnHands ?? -1);
    }
    
    public override void Update(float deltaTime)
    {
        var targetSize = Vector2.One;
        Vector2 mousePos = Raylib.GetMousePosition();
        
        if (_canInteract && !_blocked)
        {
            #region DraggingCard
            
            if (_isDragging)
            {
                Collider = new Rectangle(
                    0, 0, _canvas.Texture.Width, _canvas.Texture.Height
                );
                
                MouseTracking.Instance.HoveredNode = this;

                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    Position = new Vector2(mousePos.X - _dragOffset.X, mousePos.Y - _dragOffset.Y);
                }
                else
                {
                    DraggingCard.Instance.Card = null;
                    _isDragging = false;

                    if (_canBackToHand)
                    {
                        Vector2 worldPosition = Position;

                        if (Parent is null) 
                            BackToHands();

                        Position = worldPosition;

                        DraggingCard.Instance.IndexCardOnHands = 0;
                        SceneManager.Instance.PeekScene().RemoveNode(this);
                        MouseTracking.Instance.BlockedHover = false;
                    }
                }
            }

            if (IsMouseOver() || _isDragging)
            {
                targetSize = new Vector2(1.2f, 1.2f);
                Order = 200;

                if (IsMousePressed())
                {
                    DraggingCard.Instance.Card = this;
                    DraggingCard.Instance.IndexCardOnHands = _hands.Childrens.ToList().IndexOf(this);

                    _isDragging = true;
                    _dragOffset = new Vector2(mousePos.X - Position.X, mousePos.Y - Position.Y);

                    MouseTracking.Instance.BlockedHover = true;

                    Vector2 worldPosition = Position;

                    ExParent = Parent;
                    this.SetParent( SceneManager.Instance.PeekScene() );

                    Position = worldPosition;
                }

                if (IsMouseSecondDown()) _showInfo = true;
            }
            else Order = 100;

            #endregion
        }
        
        float t = 1.0f - MathF.Exp(-18f * deltaTime);
        Scale = Vector2.Lerp(Scale, targetSize, t);
        
        Effect?.Update(deltaTime, this);
        Highlight?.Update(deltaTime, this);
    }

    private bool _showInfo;
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
            
        Effect?.Draw(this);
        Highlight?.Draw(this);
        
        if (_blocked)
        {
            Raylib.DrawRectangleRounded(
                new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height), 
                0.2f, 
                10,
                new Color(0, 0, 0, 120)
            );
        }

        // TODO: rewrite in method and put to UPDATE
        if (_showInfo)
        {
            FontFamily testFont = new FontFamily()
            {
                Color = Color.White,
                Font = FontFamily.Font,
                Rotation = 0f,
                Size = 32,
                Spacing = 2f
            };
            
            float overlayWidth = 260f;
            float overlayHeight = 40f;
            float margin = 10f;

            float centerX = Bounds.X + (Bounds.Width - overlayWidth) / 2f;
            float topY = Bounds.Y - margin - overlayHeight;
            float bottomY = Bounds.Y + Bounds.Height + margin;

            float overlayY = topY;
            
            
            float textHeight = Engine.Helpers.Text.CalculateWrappedTextHeight(
                testFont, 
                Description,
                overlayWidth - 20f
            );
            
            overlayHeight += textHeight + testFont.Size / 2;

            if (topY < 0)
            {
                overlayY = bottomY;
                if (overlayY + overlayHeight > Raylib.GetScreenHeight())
                {
                    overlayY = Raylib.GetScreenHeight() - overlayHeight - margin;
                }
            }

            Raylib.DrawRectangleRounded(
                new Rectangle(centerX, overlayY - (textHeight + testFont.Size / 2), overlayWidth, overlayHeight), 
                0.2f, 
                10,
                new Color(25, 25, 25, 255)
            );
            
            Raylib.DrawRectangleRoundedLinesEx(
                new Rectangle(centerX, overlayY - (textHeight + testFont.Size / 2), overlayWidth, overlayHeight), 
                0.2f, 
                10,
                4,
                new Color() {R = 55, G = 55, B = 55, A = 255}
            );
            
            float textCenterX = centerX + overlayWidth / 2f;
            float firstTextY = overlayY - (textHeight + testFont.Size / 2) + 20f;
            float secondTextY = firstTextY + 30f;
            
            Engine.Helpers.Text.DrawPro(
                testFont, 
                Name, 
                new Vector2(textCenterX, firstTextY)
            );

            
          
            
            Engine.Helpers.Text.DrawWrapped(
                testFont, 
                Description, 
                new Vector2(textCenterX - (overlayWidth - 20f) / 2f, secondTextY), 
                overlayWidth - 20f, 
                TextAlignment.Center
            );

            // string additionalText = ;
            // Raylib.DrawTextPro(
            //     FontFamily.Font,
            //     additionalText,
            //     new Vector2(textCenterX, secondTextY),
            //     new Vector2(Raylib.MeasureTextEx(FontFamily.Font, additionalText, FontFamily.Size, 3).X / 2, 0),
            //     0f,
            //     FontFamily.Size,
            //     3,
            //     Color.White
            // );
            
            _showInfo = false;
        }

        if (IsOnBurningAnimation)
        {
            Raylib.DrawRectangleRounded(
                new Rectangle(
                    Bounds.X, 
                    (Bounds.Y + DefaultSize.Y) - _redHeight, 
                    Bounds.Width, 
                    Bounds.Height - DefaultSize.Y + _redHeight
                ), 
                0.2f, 
                10,
                new Color(120, 0, 0, 120)
            );
        }
    }

    private bool _isBurningAnimation;
    public bool IsOnBurningAnimation => _isBurningAnimation;
    public async void BurnCard()
    {
        _isBurningAnimation = true;
        _canBackToHand = false;
        _canInteract = false;

        ClearDependencies();
        
        try
        {
            Console.WriteLine($"Карта Начала сжигание");
            
            await BurnCardAsyncAnimation();
            Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сжигании карты: {ex}");
        }
        finally
        {
            Console.WriteLine($"Карта Закончила сжигание");
            _isBurningAnimation = false;
        }
    }

    private float _redHeight = 0;
    public async Task BurnCardAsyncAnimation()
    {
        while (_redHeight < DefaultSize.Y)
        {
            _redHeight += 8f;
            await Task.Delay(20); 
        }
    }

    public void ClearDependencies()
    {
        if (DraggingCard.Instance.Card == this)
            DraggingCard.Instance.Card = null;
        
        MouseTracking.Instance.BlockedHover = false;
        if (MouseTracking.Instance.HoveredNode == this)
            MouseTracking.Instance.HoveredNode = null;
    }

    public override void Dispose()
    {
        if (Parent is not null)
        {
            Parent.RemoveChild(this);
            SetParent(null);
        }
        else if (SceneManager.Instance.PeekScene() is not null && SceneManager.Instance.PeekScene().ContainsNode(this))
            SceneManager.Instance.PeekScene().RemoveNode(this);
        
        ClearDependencies();
        ClearChildrens();
    }
}