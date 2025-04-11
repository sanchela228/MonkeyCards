using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using Raylib_cs;

namespace MonkeyCards.Game.Scenes;

public class Menu : Scene
{
    private Rectangle nextSceneButton;
    private Rectangle backButton;
    private Rectangle exitButton;
    
    public Menu()
    {
        nextSceneButton = new Rectangle(100, 100, 200, 50);
        backButton = new Rectangle(100, 170, 200, 50);
        exitButton = new Rectangle(100, 240, 200, 50);
    }
    
    public override void Update(float deltaTime)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, nextSceneButton))
        {
            Console.WriteLine("TEST");
            Manager.Instance.PushScene(new Test());
        }

        if (Manager.Instance.HasPreviousScene() &&
            Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, backButton))
        {
            Manager.Instance.PopScene();
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, exitButton))
        {
            Raylib.CloseWindow();
        }
    }
    
    public override void Draw()
    {
        Raylib.ClearBackground(Color.RayWhite);

        Raylib.DrawRectangleRec(nextSceneButton, Color.Blue);
        Raylib.DrawText("Next", (int)nextSceneButton.X + 10, (int)nextSceneButton.Y + 15, 20, Color.White);

        if (Manager.Instance.HasPreviousScene())
        {
            Raylib.DrawRectangleRec(backButton, Color.Green);
            Raylib.DrawText("Back", (int)backButton.X + 70, (int)backButton.Y + 15, 20, Color.White);
        }

        Raylib.DrawRectangleRec(exitButton, Color.Red);
        Raylib.DrawText("Exit", (int)exitButton.X + 70, (int)exitButton.Y + 15, 20, Color.White);
    }
    
    public override void Dispose()
    {
        
    }
}