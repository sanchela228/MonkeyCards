using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Controllers;
using MonkeyCards.Game.Nodes.Game.Models;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table;

public class Placeholder : Node
{
    public Vector2 DefaultSize => new Vector2(136f, 206f);
    public Placeholder()
    {
        Size = DefaultSize;
    }
    public override void Update(float deltaTime)
    {
        if (IsMouseOverWithoutOverlap())
        {
            if (DraggingCard.Instance.Card is Card && !Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                if (!_childrens.Any())
                {
                    DraggingCard.Instance.Card.SetParent(this);
                }
                else
                {
                    if (DraggingCard.Instance.Card.ExParent is not null)
                    {
                        _childrens.First().SetParent(DraggingCard.Instance.Card.ExParent);
                        DraggingCard.Instance.Card.SetParent(this);
                    }
                }
            }
        }

        if (_childrens.Any())
        {
            // Console.WriteLine(_childrens.First());
            _childrens.First().Position = this.Position;
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
            new Color(0,0,0,55)
        );
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}