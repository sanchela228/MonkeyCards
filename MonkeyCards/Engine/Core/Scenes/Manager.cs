namespace MonkeyCards.Engine.Core.Scenes;

public class Manager
{
    static Manager() => Instance = new();
    public static Manager Instance { get; private set; }
    
    private readonly Stack<Scene> _scenes = new();
    public void PushScene(Scene scene) => _scenes.Push(scene);
    public void PopScene() => _scenes.Pop()?.RootDispose();
    public void Update(float deltaTime) => _scenes.Peek()?.RootUpdate(deltaTime);
    public void Draw() => _scenes.Peek()?.RootDraw();
    public void Dispose()
    {
        while (_scenes.Count > 0)
            PopScene();
    }
    public bool HasPreviousScene() => _scenes.Count > 1;
}