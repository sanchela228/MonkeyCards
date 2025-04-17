using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Nodes.Game.Table.Combo;
using ComboElement = MonkeyCards.Game.Nodes.Game.Table.Combo.Element;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table;

public class Table : Node
{
    protected Line? _comboLine;
    public Table(Vector2 centerPoint)
    {
        Position = centerPoint;
        AddChildrens( new List<Placeholder>
        {
            new Placeholder(),
            new Placeholder(),
            new Placeholder(),
            new Placeholder(),
            new Placeholder()
        } );
        
        if ( Childrens.Any() )
            Visuals.Render.PlaceInLine( Childrens.OfType<Placeholder>(), (int) Childrens[0].Size.X, Position, 20 );

        _comboLine = new Line();
        _comboLine.Position += new Vector2(0, -(Placeholder.DefaultSize.Y / 2 + 40 ));
        
        AddChild( _comboLine );
    }
    
    public override void Update(float deltaTime)
    {
        // TODO: add cache data for List ComboElement, and rewrite font in constructor
        
        var els = new List<Node>();
        Childrens.OfType<Placeholder>().ToList().ForEach(x =>
        {
            if (x.Childrens.Any())
            {
                Card test = (Card) x.Childrens.First();
                els.Add( new ComboElement( _comboLine.Font ) );
            }
        });

        _comboLine.Childrens = els;
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