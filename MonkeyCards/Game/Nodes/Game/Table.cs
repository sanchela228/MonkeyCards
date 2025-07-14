using System.Diagnostics.CodeAnalysis;
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
    
    public static int CountPlaceholders => 5;
    private static List<Placeholder> _placeholders;
    
    public Table(Vector2 centerPoint)
    {
        Position = centerPoint;
        
        List<Placeholder> placeholders = new();
        for (int i = 0; i < CountPlaceholders; i++)
            placeholders.Add( new Placeholder(i) );
        
        _comboLine = new Line();
        _comboLine.Position += new Vector2(0, -(Placeholder.DefaultSize.Y / 2 + 40 ));
        
        AddChild( _comboLine );
        
        AddChildrens(placeholders);
        _placeholders = placeholders;
        
        Visuals.Render.PlaceInLine( 
            Childrens.OfType<Placeholder>(), 
            (int) Childrens.OfType<Placeholder>().First().Size.X, 
            Position, 
            20 
        );
    }

    public static Card GetCardFromPlaceholder(int index)
    {
        if (index < 1 || index >= _placeholders.Count)
            return null;
        
        var placeholder = _placeholders[index];
        
        if (placeholder.Childrens.Count == 0)
            return null;
        
        return placeholder.Childrens[0] as Card;
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