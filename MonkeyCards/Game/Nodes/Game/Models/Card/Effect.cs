namespace MonkeyCards.Game.Nodes.Game.Models.Card;

public abstract class Effect
{
    public abstract void Update(float deltaTime);
    public abstract void Draw();
}