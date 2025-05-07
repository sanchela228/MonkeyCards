using System.Numerics;
using Engine.Core.Objects;
using Game.Nodes.Game.Models.Card;
using Game.Nodes.Game.Table.Combo;
using ComboElement = Game.Nodes.Game.Table.Combo.Element;
using Raylib_cs;

namespace Game.Nodes.Game.Table;

public class Table : Node
{
    protected Line? _comboLine;
    public Table(Vector2 centerPoint)
    {
        Position = centerPoint;
        AddChildrens( new List<Placeholder>
        {
            new Placeholder(0),
            new Placeholder(1),
            new Placeholder(2),
            new Placeholder(3),
            new Placeholder(4)
        } );
        
        if ( Childrens.Any() )
            Visuals.Render.PlaceInLine( Childrens.OfType<Placeholder>(), (int) Childrens[0].Size.X, Position, 20 );

        _comboLine = new Line();
        _comboLine.Position += new Vector2(0, -(Placeholder.DefaultSize.Y / 2 + 40 ));
        
        AddChild( _comboLine );
    }
    
    public override void Update(float deltaTime)
    {
        // TODO: add cache data for List ComboElement
        
        var els = new List<Node>();
        
        List<Card> cardsPull = new List<Card>();

        GetCards().ForEach(card =>
        {
            for (int i = 0; i < card.Multiply; i++)
            {
                cardsPull.Add(card);
                    
                els.Add( new ComboElement( card.Symbol, card.Suit )
                {
                    Size = new Vector2(10, 20)
                } );
            }
        });

        if (els.Count > 0)
        {
            var res = new Result(cardsPull);
            els.Insert(0, res);
        }
        
        _comboLine.ReplaceChildrens(els);
    }

    public List<Card> GetCards()
    {
        var cards = new List<Card>();
        
        Childrens.OfType<Placeholder>().ToList().ForEach(x =>
        {
            if (x.Childrens.Any())
                cards.Add( (Card) x.Childrens.First() );
        });

        return cards;
    }

    public void Clear()
    {
        Childrens.OfType<Placeholder>().ToList().ForEach(x =>
        {
            if (x.Childrens.Any() && x.Childrens.First() is Card card)
            {
                x.ClearChildrens();
                card.Dispose();
            }
        });
    }

    public override void Draw() 
    {
        // Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 20, 20, Color.Black);
    }

    public override void Dispose()
    {
        // throw new NotImplementedException();
    }
}