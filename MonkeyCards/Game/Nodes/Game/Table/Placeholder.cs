using System.Numerics;
using Engine.Core.Objects;
using Game.Controllers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Nodes.Game.Table;

public class Placeholder : Node
{
    public static Vector2 DefaultSize => new Vector2(136f, 206f);
    public Placeholder()
    {
        Size = DefaultSize;
    }

    private Color _color;
    private bool _hoverWithCardEffect = false;
    public override void Update(float deltaTime)
    {
        _hoverWithCardEffect = false;
        
        if (IsMouseOverWithoutOverlap())
        {
            if (DraggingCard.Instance.Card is Card && !_childrens.Any()) 
                _hoverWithCardEffect = true;
            
            if (DraggingCard.Instance.Card is Card && !Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                if (!_childrens.Any())
                {
                    Vector2 worldPosition = DraggingCard.Instance.Card.Position;
                    DraggingCard.Instance.Card.SetParent(this);
                    DraggingCard.Instance.Card.Position = worldPosition;
                }
                else
                {
                    if (DraggingCard.Instance.Card.ExParent is not null)
                    {
                        Vector2 worldPosition = DraggingCard.Instance.Card.Position;
                        Vector2 worldChildPosition = _childrens.First().Position;

                        var index = DraggingCard.Instance.IndexCardOnHands;
                        
                        _childrens.First().SetParent(DraggingCard.Instance.Card.ExParent, index ?? -1, worldChildPosition);
                        
                        DraggingCard.Instance.Card.SetParent(this);
                        DraggingCard.Instance.Card.Position = worldPosition;
                    }
                }
            }
        }
        
        _color = _hoverWithCardEffect ? new Color(0,0,0,155) : new Color(0,0,0,55);

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