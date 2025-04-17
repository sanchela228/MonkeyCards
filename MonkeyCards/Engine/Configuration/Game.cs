using MonkeyCards.Engine.Managers;
using MonkeyCards.Engine.Utilites;

namespace MonkeyCards.Engine.Configuration;
public class Game
{
    public required int Version { get; set; }
    public required string VersionName { get; set; }
    public required string PathResourcesFolder { get; set; }
    
    public int WindowStartWidth { get; set; } 
    public int WindowStartHeight { get; set; }

    public Game()
    {
        Env.Load();

        Resources.Instance.RootFolderPath = PathResourcesFolder ?? Env.Get("RESOURCES_PATH_FOLDER");
        
        WindowStartWidth = int.Parse(Env.Get("WINDOW_START_WIDTH"));
        WindowStartHeight = int.Parse(Env.Get("WINDOW_START_HEIGHT"));
    }
}