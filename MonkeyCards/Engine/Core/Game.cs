using System.Resources;
using Engine.Core.Scenes;
using Engine.Managers;
using Engine.Utilites;
using SceneManager = Engine.Core.Scenes.Manager;
using GameConfig = Engine.Configuration.Game;
using Raylib_cs;

namespace Engine.Core;

public class Game : IDisposable
{
    public readonly GameConfig Config;
    
    public Game(GameConfig config)
    {
        Config = config;
        Raylib.InitWindow(config.WindowStartWidth, config.WindowStartHeight, "MonkeyCards");
        // Raylib.SetTargetFPS(120);
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
            
            // Raylib.DrawFPS(10, 10);
            
            Raylib.EndDrawing();
        }
    }

    public void Dispose()
    {
        SceneManager.Instance.Dispose();
        Raylib.CloseWindow();
    }
}