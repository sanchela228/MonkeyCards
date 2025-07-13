using System.Numerics;
using Engine.Core.Objects;
using Game.Controllers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Nodes.Game.Table;

public class Placeholder : Node
{
    public int Index { get; set; }
    public static Vector2 DefaultSize => new Vector2(136f, 206f);
    public Placeholder(int index)
    {
        Index = index;
        Size = DefaultSize;
    }

    private Color _color;
    private bool _hoverWithCardEffect = false;
    private bool _isMouseOver;

    public override void Update(float deltaTime)
    {
        // TODO: rewrite this code more clean
        
        bool wasMouseOverLastFrame = _isMouseOver;
        _isMouseOver = IsMouseOverWithoutOverlap();
        _hoverWithCardEffect = false;
        
        if (_isMouseOver && !wasMouseOverLastFrame && DraggingCard.Instance.Card is Card { Special: not null } cx)
            cx.Special.OnStartHover(cx, this, Index);
        
        if (_isMouseOver)
        {
            if (DraggingCard.Instance.Card is Card { Special: not null } c)
                c.Special.OnHover(c, this, Index);
            
            if (DraggingCard.Instance.Card is Card && _childrens.Count == 0)
                _hoverWithCardEffect = true;
            
            if (DraggingCard.Instance.Card is Card card && !Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                if (!_childrens.Any())
                {
                    Vector2 worldPosition = DraggingCard.Instance.Card.Position;
                    
                    if (card.Special is not null)
                        card.Special.OnPlay(card);
                    
                    if (DraggingCard.Instance.Card is not null)
                    {
                        DraggingCard.Instance.Card.SetParent(this);
                        DraggingCard.Instance.Card.Position = worldPosition;
                    }
                    else card.Position = worldPosition;
                }
                else
                {
                    if (DraggingCard.Instance.Card.ExParent is not null)
                    {
                        Vector2 worldPosition = DraggingCard.Instance.Card.Position;
                        Vector2 worldChildPosition = _childrens.First().Position;

                        var index = DraggingCard.Instance.IndexCardOnHands;
                        
                        _childrens.First().SetParent(DraggingCard.Instance.Card.ExParent, index ?? -1, worldChildPosition);
                        
                        if (card.Special is not null)
                            card.Special.OnPlay(card);

                        if (DraggingCard.Instance.Card is not null)
                        {
                            DraggingCard.Instance.Card.SetParent(this);
                            DraggingCard.Instance.Card.Position = worldPosition;
                        }
                        else card.Position = worldPosition;
                    }
                }
            }
        }
        else if (wasMouseOverLastFrame)
        {
            if (DraggingCard.Instance.Card is Card { Special: not null } c)
                c.Special.OnEndHover(c, this, Index);
        }

        if (_hoverWithCardEffect)
        {
            _color = new Color(0,0,0,155);
        }
        else
        {
            _color = new Color(0,0,0,55);
        }

        if (_childrens.Any())
        {
            float t = 1.0f - MathF.Exp(-18f * deltaTime);
            _childrens.First().Position = Vector2.Lerp(_childrens.First().Position, Position, t);
        }
    }

    public override void Draw()
    {
        var rect = new Rectangle(
            (int)Bounds.X, 
            (int)Bounds.Y, 
            (int)Bounds.Width, 
            (int)Bounds.Height
        );
        
        Raylib.DrawRectangleRounded(
            rect,
            0.2f,
            10,
            _color
        );
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}