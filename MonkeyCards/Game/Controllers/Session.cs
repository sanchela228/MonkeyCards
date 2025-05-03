using MonkeyCards.Game.Nodes.Game;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Nodes.Game.Table;
using MonkeyCards.Game.Services;

namespace MonkeyCards.Game.Controllers;

public class Session
{
    public Player Self { get; protected set; }
    public Player RemotePlayer { get; protected set; }

    public int StartStack { get; protected set; } = 5;

    private int _round = 1;
    
    public void Init(Hands hands, Table table, IEnumerable<Card> startCards)
    {
        Self = new Player(hands)
        {
            Table = table
        };
            
        Self.Hands.AddCards( startCards );
    }

    public void EndRound(float money)
    {
        Self.Money += money;
        
        _round++;
        
        PickToHandRoundCards();
    }
    
    public void EndRound()
    {
        Self.Money += CardsHolder.CalcCombo(Self.Table.GetCards());
        
        _round++;

        PickToHandRoundCards();
    }

    public void PickToHandRoundCards()
    {
        if (Self.Hands.Childrens.Count <= Self.Hands.MaxCards)
        {
            if ( (Self.Hands.MaxCards - Self.Hands.Childrens.Count) == 1 )
                Self.Hands.AddCard( CardsHolder.Instance.Defaults.Pop() );
            else
            {
                Self.Hands.AddCards( CardsHolder.Instance.TakeFromTop(2) );
            }
        }
    }

    public void SellCard(Card card)
    {
        Self.Money += MathF.Round(card.Cost / 2, 1);
        
        card.BurnCard();
    }
    
    
    static Session() => Instance = new();
    public static Session Instance { get; private set; }
}