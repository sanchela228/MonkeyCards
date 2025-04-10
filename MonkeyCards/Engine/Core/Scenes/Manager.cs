namespace MonkeyCards.Engine.Core.Scenes;

public class Manager
{
    static Manager() => Instance = new();
    public static Manager Instance { get; private set; }
    
    private readonly Stack<IScene> _scenes = new();
    public void PushScene(IScene scene) => _scenes.Push(scene);
    public void PopScene() => _scenes.Pop()?.Dispose();
    public void Update(float deltaTime) => _scenes.Peek()?.Update(deltaTime);
    public void Draw() => _scenes.Peek()?.Draw();
    public void Dispose()
    {
        while (_scenes.Count > 0)
            PopScene();
    }
    public bool HasPreviousScene() => _scenes.Count > 1;
}