using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using MonkeyCards.Game.Nodes.Game;
using MonkeyCards.Game.Nodes.Game.Models;
using Raylib_cs;

namespace MonkeyCards.Game.Scenes;

public class Test : Scene
{
    private Rectangle backButton;
    private Rectangle exitButton;
    // private Card _cardTest;
    
    public Hands Hands = new Hands( new Vector2(400, 420), 200 );
    public Test()
    {
        backButton = new Rectangle(100, 170, 200, 50);
        exitButton = new Rectangle(100, 240, 200, 50);
        
        Visuals.BackgroundColorize.Instance.SetSettings();
        
        _nodes.Add(Hands);
        Hands.AddChildrens( new List<Card>
        {
            new Card("test_7", "A"),
            new Card("test_7", "K"),
            new Card("test_7", "Q"),
            new Card("test_7", "J"),
            new Card("test_7", "10"),
            new Card("test_7", "8"),
            new Card("test_7", "5"),
        });
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
        
        
        Raylib.DrawRectangle(
            (int)Hands.Position.X,
            (int)Hands.Position.Y,
            (int)Hands.Bounds.Width,
            (int)Hands.Bounds.Height,
            Color.Yellow
        );
    }
    
    public override void Dispose()
    {
        Visuals.BackgroundColorize.Instance.UnloadShader();
    }
}