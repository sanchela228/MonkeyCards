using MonkeyCards.Engine.Core;
using MonkeyCards.Game.Scenes;

var config = new MonkeyCards.Engine.Configuration.Game()
{
    Version = 2,
    VersionName = "low_development"
};

using var game = new Game( config );
game.Run( new Test() );