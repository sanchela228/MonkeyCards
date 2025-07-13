using Game.Controllers;
using Game.Nodes.Game.Table;

namespace Game.Nodes.Game.Models.Card.Specials;

public class TwoSideCheckAndSwapSuits : Special
{
    public override bool Removable { get; set; }
    
    Card? lcard;
    Card? rcard;

    public override void OnStartHover(Card card, Placeholder placeholder, int index)
    {
        Console.WriteLine("OnDraw");
    }

    public override void OnHover(Card card, Placeholder placeholder, int index)
    {
        if (!placeholder.Childrens.Any())
        {
            if (index > 1 && index < Table.Table.CountPlaceholders)
            {
                lcard = Table.Table.GetCardFromPlaceholder(index - 1);
                rcard = Table.Table.GetCardFromPlaceholder(index + 1);

                if (lcard is not null && rcard is not null)
                {
                    rcard.Highlight = Highlight.Default();
                    lcard.Highlight = Highlight.Default();
                }
            }
            
            Console.WriteLine("OnHover");
        }
    }

    public override void OnEndHover(Card card, Placeholder placeholder, int index)
    {
        if (lcard != null && rcard != null)
        {
            rcard.Highlight = null;
            lcard.Highlight = null;
        }
        
        Console.WriteLine("OnEndHover");
    }

    public override void OnPlay(Card card)
    {
        if (lcard != null && rcard != null)
        {
            rcard.Highlight = null;
            lcard.Highlight = null;

            CardSuit lSuit = lcard.Suit;
            CardSuit rSuit = rcard.Suit;
            
            View lView = lcard.View;
            View rView = rcard.View;
            
            lcard.View = lView with { Texture = rView.Texture };
            rcard.View = rView with { Texture = lView.Texture };
            
            lcard.Suit = rSuit;
            rcard.Suit = lSuit;
            
            card.BurnCard();
        }
        else
        {
            DraggingCard.Instance.Clear();
            card.BackToHands();
        }
        
        Console.WriteLine("OnPlay");
    }
}