using SceneManager = MonkeyCards.Engine.Core.Scenes.Manager;
using GameConfig = MonkeyCards.Engine.Configuration.Game;
using Raylib_cs;

namespace MonkeyCards.Engine.Core;

public class Game : IDisposable
{
    public Game(GameConfig config)
    {
        Raylib.InitWindow(config.WindowStartWidth, config.WindowStartHeight, "MonkeyCards");
        Raylib.SetTargetFPS(120);
            
        SceneManager.Instance.PushScene(config.StartScene);
    }
    
    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
                
            SceneManager.Instance.Update(deltaTime);
                
            Raylib.BeginDrawing();
            
            SceneManager.Instance.Draw();
            
            Raylib.EndDrawing();
        }
    }

    public void Dispose()
    {
        SceneManager.Instance.Dispose();
        Raylib.CloseWindow();
    }
}