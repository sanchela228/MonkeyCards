namespace Game.Nodes.Game.Models.Card.Specials;

public class TwoSideCheckAndSwapSuits : Special
{
    public override bool Removable { get; set; }

    public override void OnDraw(Card card)
    {
        Console.WriteLine("OnDraw");
    }

    public override void OnHover(Card card)
    {
        Console.WriteLine("OnHover");
    }

    public override void OnPlay(Card card)
    {
        Console.WriteLine("OnPlay");
    }
}