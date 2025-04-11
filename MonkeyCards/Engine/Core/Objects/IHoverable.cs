namespace MonkeyCards.Engine.Core.Objects;

public interface IHoverable
{
    void OnHoverEnter();
    void OnHoverExit();
    bool IsHoverable { get; set; }
}