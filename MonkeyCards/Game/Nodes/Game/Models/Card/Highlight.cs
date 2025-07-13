using Game.Nodes.Game.Models.Card.Highlights;
using Raylib_cs;

namespace Game.Nodes.Game.Models.Card;

public abstract class Highlight
{ 
    public bool Active { get; set; }
    public static Highlight Default() => new Default();
    
    public abstract void Update(float deltaTime, Card card);
    public abstract void Draw(Card card);
    
}