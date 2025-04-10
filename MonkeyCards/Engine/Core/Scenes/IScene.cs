namespace MonkeyCards.Engine.Core.Scenes;

public interface IScene
{
    void Update(float deltaTime);
    void Draw();
    void Dispose();
}