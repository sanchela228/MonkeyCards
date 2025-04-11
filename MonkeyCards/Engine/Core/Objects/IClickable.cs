namespace MonkeyCards.Engine.Core.Objects;

public interface IClickable
{
    void OnClick();
    bool IsClickable { get; set; }
}