using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using Raylib_cs;

namespace MonkeyCards.Game.Scenes;

public class Test : Scene
{
    private Rectangle backButton;
    private Rectangle exitButton;
    private Card _cardTest;
    
    public Test()
    {
        backButton = new Rectangle(100, 170, 200, 50);
        exitButton = new Rectangle(100, 240, 200, 50);
        
        Visuals.BackgroundColorize.Instance.SetSettings();
        
        // _cardTest = new Default("test", "A");
    }
    
    public override void Update(float deltaTime)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

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
        
        Visuals.BackgroundColorize.Instance.BeforeDrawing();
        
    }
    
    public override void Draw()
    {
        Visuals.BackgroundColorize.Instance.Draw();
        
        if (Manager.Instance.HasPreviousScene())
        {
            Raylib.DrawRectangleRec(backButton, Color.Blue);
            Raylib.DrawText("Back", (int)backButton.X + 70, (int)backButton.Y + 15, 20, Color.White);
        }
        
        Raylib.DrawRectangleRec(exitButton, Color.Red);
        Raylib.DrawText("Exit", (int)exitButton.X + 70, (int)exitButton.Y + 15, 20, Color.White);
        
        // _cardTest.Draw();
    }
    
    public override void Dispose()
    {
        Visuals.BackgroundColorize.Instance.UnloadShader();
    }
}