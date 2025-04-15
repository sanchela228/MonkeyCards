using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using MonkeyCards.Game.Nodes.Game;
using MonkeyCards.Game.Nodes.Game.Models;
using MonkeyCards.Game.Nodes.Game.Table;
using Raylib_cs;

namespace MonkeyCards.Game.Scenes;

public class Test : Scene
{
    private Rectangle backButton;
    private Rectangle exitButton;
    
    public readonly Hands Hands = new Hands( 
        new Vector2(Raylib.GetRenderWidth() / 2, Raylib.GetRenderHeight() * 0.8f), 200 
    );
    public Test()
    {
        Visuals.BackgroundColorize.Instance.SetSettings();
        
        Hands.AddChildrens( new List<Card>
        {
            new Card("test_1", "A", Hands),
            new Card("test_2", "K", Hands),
            new Card("test_3", "Q", Hands),
            new Card("test_4", "J", Hands),
            new Card("test_5", "10", Hands),
            new Card("test_6", "8", Hands),
            new Card("test_7", "5", Hands),
        });
        _nodes.Add(Hands);
        
        _nodes.Add( new Table(new Vector2(Raylib.GetRenderWidth() / 2, Raylib.GetRenderHeight() * 0.3f)) );
        
      
    }
    
    public override void Update(float deltaTime)
    {
        Visuals.BackgroundColorize.Instance.BeforeDrawing();
    }
    
    public override void Draw()
    {
        Visuals.BackgroundColorize.Instance.Draw();
    }
    
    public override void Dispose()
    {
        Visuals.BackgroundColorize.Instance.UnloadShader();
    }
}