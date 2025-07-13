using Raylib_cs;

namespace Game.Nodes.Game.Models.Card.Highlights;

public class Default : Highlight
{
    public override void Update(float deltaTime, Card node)
    {
        
    }

    public override void Draw(Card card)
    {
        Raylib.DrawRectangleRounded(
            new Rectangle(card.Bounds.X, card.Bounds.Y, card.Bounds.Width, card.Bounds.Height), 
            0.2f, 
            10,
            new Color(50, 90, 200, 120)
        );
    }
}