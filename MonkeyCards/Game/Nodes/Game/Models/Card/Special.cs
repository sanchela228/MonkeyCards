namespace Game.Nodes.Game.Models.Card;

abstract public class Special
{
    public abstract bool Removable { get; set; }
    public abstract void OnDraw(Card card);
    public abstract void OnHover(Card card);
    public abstract void OnPlay(Card card);

    public virtual void OnRemove(Card card)
    {
        if (Removable) card.BurnCard();
    }
}