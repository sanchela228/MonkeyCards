using Engine.Utilites;
using Game.Scenes;

var config = new Engine.Configuration.Game()
{
    Version = 2,
    VersionName = "low_development",
    PathResourcesFolder = Env.Get("RESOURCES_PATH_FOLDER") ?? "Resources",
};

using var game = new Engine.Core.Game(config);
game.Run( new Test() );