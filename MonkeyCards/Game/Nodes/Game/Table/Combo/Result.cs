using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table.Combo;

public class Result : Node
{
    protected Font _font;
    
    protected float fontSize = 42f;

    protected float Cost = 0;

    protected FontFamily FontFamily = new()
    {
        Size = 42,
        Font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42),
        Rotation = 0,
        Spacing = 3f,
        Color = Color.Black
        
    };
    public Result(IEnumerable<Card> cards)
    {
        Console.WriteLine("RESULT");
        
        Cost += cards.Sum(x => x.Cost);
        
    }
    
    public override void Update(float deltaTime)
    {
      
    }

    public override void Draw()
    {
        Raylib.DrawTextPro( 
            FontFamily.Font, 
            Cost + "$ = ", 
            new Vector2(Position.X, Position.Y),
            new Vector2(21, 21),
            FontFamily.Rotation,
            FontFamily.Size,
            FontFamily.Spacing,
            Color.Black
        );
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }
}