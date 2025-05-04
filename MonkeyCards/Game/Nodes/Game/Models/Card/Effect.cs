using Raylib_cs;

namespace Game.Nodes.Game.Models.Card;

public enum EffectType
{
    Shake, Glow
}

public abstract class Effect
{
    public abstract EffectType Type { get; }
    
    public abstract void Start(Card card, Rectangle placeholder);
    public abstract void Update(float deltaTime, Card card);
    public abstract void Draw(Card card);
}
// temp test classes
public class ShakeEffect : Effect
{
    public override EffectType Type => EffectType.Shake;
    public float Intensity { get; set; }
    public float Duration { get; set; }

    public override void Start(Card card, Rectangle placeholder) { }
    public override void Update(float deltaTime, Card card) { }
    public override void Draw(Card card) { }
}

public class GlowEffect : Effect
{
    public override EffectType Type => EffectType.Glow;
    public string Color { get; set; }
    public float Radius { get; set; }

    public override void Start(Card card, Rectangle placeholder) { }
    public override void Update(float deltaTime, Card card) {  }
    public override void Draw(Card card) {  }
}