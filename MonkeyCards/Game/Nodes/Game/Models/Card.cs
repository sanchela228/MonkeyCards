using System.Numerics;
using System.Text.Json.Serialization;
using MonkeyCards.Engine.Managers;
using SceneManager = MonkeyCards.Engine.Core.Scenes.Manager;
using MonkeyCards.Game.Controllers;
using MonkeyCards.Game.Helpers.Converters;
using MonkeyCards.Game.Scenes;
using Raylib_cs;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace MonkeyCards.Game.Nodes.Game.Models.Card;
using Engine.Core.Objects;

public class Card : Node
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Char Symbol { get; set; }
    public CardSuit Suit { get; set; }
    public float Cost { get; set; }
    public int Multiply { get; set; } = 1;
    public Border Border { get; set; } = Border.Default;
    public FontFamily FontFamily { get; set; }
    public View View { get; set; }
    public Effect? Effect { get; set; }

    public string ShortName
    {
        get
        {
            var isNumber = false;
            var s = Symbol.ToString();
            
            if (s == "2" || s == "3")
                isNumber = true;
            
            if (isNumber && Multiply > 1)
                return Multiply + "x" + Symbol;
            
            if (!isNumber && Multiply == 2)
                return Symbol.ToString() + Symbol;
                
            if (Multiply > 2)
                return Multiply + "x" + Symbol;
            
            return Symbol.ToString();
        }
    }

    protected RenderTexture2D _canvas;

    protected Font _font;
    protected Texture2D _icon;
    
    protected Hands? _hands { get; set; }
    public Vector2 DefaultSize => new(136f, 206f);
    
    public Card( Guid Id, string Name, Char Symbol, CardSuit Suit, float Cost, FontFamily FontFamily, 
        View View, string Description = null, int multiply = 1, Border Border = Border.Default, Effect? Effect = null )
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
        this.Effect = Effect;
        
        PreventParent += node =>
        {
            if (node is not null && node is Hands)
                _hands = (Hands) node;
        };
        
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

            Raylib.EndTextureMode();
        
        #endregion
        
        Effect?.Start(this, placeholder);
    }
    
    protected bool _isDragging = false;
    protected bool _canInteract = true;
    protected Vector2 _dragOffset;

    public Node ExParent;


    private bool _canBackToHand = true;

    protected void BackToHands()
    {
        if ( _hands is not null && _canBackToHand)
            SetParent(_hands, DraggingCard.Instance.IndexCardOnHands ?? -1);
    }
    
    public override void Update(float deltaTime)
    {
        // TODO: change cursor view logic

        var targetSize = Vector2.One;
        Vector2 mousePos = Raylib.GetMousePosition();
        
        if (_canInteract)
        {
            #region DraggingCard
            
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
        }
        
        float t = 1.0f - MathF.Exp(-18f * deltaTime);
        Scale = Vector2.Lerp(Scale, targetSize, t);
        
        Effect?.Update(deltaTime, this);
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
        
        if (Raylib.GetRandomValue(0, 100) > 70)
        {
            // CreateSparkParticle(
            //     position: cardPosition + new Vector2(Raylib.GetRandomValue(-20,20), Raylib.GetRandomValue(-20,20)),
            //     color: new Color(255, Raylib.GetRandomValue(100,200), 0, 255)
            // );
        }
        
        Effect?.Draw(this);
    }

    private bool _isBurningAnimation = false;
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

    public async Task BurnCardAsyncAnimation()
    {
        float alpha = 1.0f;
        while (alpha > 0)
        {
            alpha -= 0.05f;
            // Raylib.SetTextureAlpha(_texture, (byte)(alpha * 255));
            await Task.Delay(50); 
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
            Parent.Childrens.Remove(this);
            SetParent(null);
        }
        else if (SceneManager.Instance.PeekScene() is not null && SceneManager.Instance.PeekScene().ContainsNode(this))
            SceneManager.Instance.PeekScene().RemoveNode(this);
        
        if (DraggingCard.Instance.Card == this)
            DraggingCard.Instance.Card = null;
        
        MouseTracking.Instance.BlockedHover = false;
        
        if (MouseTracking.Instance.HoveredNode == this)
            MouseTracking.Instance.HoveredNode = null;
            
        Childrens.Clear();
    }
}