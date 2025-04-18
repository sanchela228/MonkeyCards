namespace MonkeyCards.Game.Nodes.Game.Models.Card;

public enum EffectType
{
    Shake, Glow
}

public abstract class Effect
{
    public abstract EffectType Type { get; }
    public abstract void Update(float deltaTime);
    public abstract void Draw();
}
// temp test classes
public class ShakeEffect : Effect
{
    public override EffectType Type => EffectType.Shake;
    public float Intensity { get; set; }
    public float Duration { get; set; }

    public override void Update(float deltaTime) { }
    public override void Draw() { }
}

public class GlowEffect : Effect
{
    public override EffectType Type => EffectType.Glow;
    public string Color { get; set; }
    public float Radius { get; set; }

    public override void Update(float deltaTime) {  }
    public override void Draw() {  }
}