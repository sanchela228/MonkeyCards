using Game.Nodes.Game.Table;

namespace Game.Nodes.Game.Models.Card;

abstract public class Special
{
    public abstract bool Removable { get; set; }
    public abstract void OnStartHover(Card card, Placeholder placeholder, int index);
    public abstract void OnHover(Card card, Placeholder placeholder, int index);
    public abstract void OnPlay(Card card);
    public abstract void OnEndHover(Card card, Placeholder placeholder, int index);
    public virtual void OnRemove(Card card)
    {
        if (Removable) card.BurnCard();
    }
}