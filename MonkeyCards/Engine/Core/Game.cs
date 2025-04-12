using MonkeyCards.Engine.Core.Scenes;
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
    }
    
    public void Run( Scene startScene)
    {
        SceneManager.Instance.PushScene( startScene );

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