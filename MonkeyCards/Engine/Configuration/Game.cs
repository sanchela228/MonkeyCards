using MonkeyCards.Engine.Utilites;

namespace MonkeyCards.Engine.Configuration;

using Core.Scenes;

public class Game
{
    public int WindowStartWidth { get; set; } 
    public int WindowStartHeight { get; set; }

    public Game()
    {
        Env.Load();
        
        WindowStartWidth = int.Parse(Env.Get("WINDOW_START_WIDTH"));
        WindowStartHeight = int.Parse(Env.Get("WINDOW_START_HEIGHT"));
    }
}