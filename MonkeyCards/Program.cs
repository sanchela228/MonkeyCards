using Game.Scenes;

var config = new Engine.Configuration.Game()
{
    Version = 2,
    VersionName = "low_development",
    PathResourcesFolder = "Resources",
};

using var game = new Engine.Core.Game(config);
game.Run( new Test() );