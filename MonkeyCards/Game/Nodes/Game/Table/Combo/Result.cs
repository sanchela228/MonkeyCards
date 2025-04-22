using System.Numerics;
using MonkeyCards.Engine.Core.Objects;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Services;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Table.Combo;

public class Result : Node
{
    protected float Cost = 0;
    protected string Text = "";

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
        Cost = CardsHolder.CalcCombo(cards);
        Text = Cost + "$ =";
        Size = Raylib.MeasureTextEx(FontFamily.Font, Text, FontFamily.Size, FontFamily.Spacing);
    }
    
    public override void Update(float deltaTime)
    {
      
    }

    public override void Draw()
    {
        Raylib.DrawTextPro( 
            FontFamily.Font, 
            Text, 
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
        
    }
}