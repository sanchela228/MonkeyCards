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
        nextSceneButton = new Rectangle(100, 100, 200, 50);
        exitButton = new Rectangle(100, 240, 200, 50);
    }
    
    protected override void Update(float deltaTime)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, nextSceneButton))
        {
            Manager.Instance.PushScene(new Test());
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, exitButton))
        {
            Raylib.CloseWindow();
        }
    }
    
    protected override void Draw()
    {
        Raylib.ClearBackground( new Color(34, 128, 28) );

        Raylib.DrawRectangleRec(nextSceneButton, Color.Blue);
        Raylib.DrawText("Start game", (int)nextSceneButton.X + 10, (int)nextSceneButton.Y + 15, 20, Color.White);

        Raylib.DrawRectangleRec(exitButton, Color.Red);
        Raylib.DrawText("Exit", (int)exitButton.X + 70, (int)exitButton.Y + 15, 20, Color.White);
    }
    
    protected override void Dispose()
    {
        
    }
}