using System.Numerics;
using Engine.Core.Scenes;
using Game.Nodes.Game.Models;
using Raylib_cs;

namespace Game.Scenes;

public class Menu : Scene
{
    private Rectangle nextSceneButton;
    private Rectangle backButton;
    private Rectangle exitButton;
    
    public Menu()
    {
        
    }
    
    public override void Update(float deltaTime)
    {

    }
    
    public override void Draw()
    {
        Raylib.ClearBackground(Color.Gray);
    }
    
    public override void Dispose()
    {
        
    }
}