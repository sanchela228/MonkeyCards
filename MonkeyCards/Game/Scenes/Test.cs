using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using MonkeyCards.Game.Controllers;
using MonkeyCards.Game.Nodes.Game;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Nodes.Game.Table;
using Raylib_cs;

namespace MonkeyCards.Game.Scenes;

public class Test : Scene
{
    private Rectangle backButton;
    private Rectangle exitButton;
    
    public readonly Hands Hands = new Hands( 
        new Vector2(Raylib.GetRenderWidth() / 2, Raylib.GetRenderHeight() * 0.85f), Raylib.GetRenderWidth() * 0.8f 
    );
    
    public Rectangle testDeleteRect = new Rectangle(30, 30, 100, 100);
    public Rectangle testAddRect = new Rectangle(230, 30, 100, 100);
    
    
    public Test()
    {
        Visuals.BackgroundColorize.Instance.SetSettings();
        
        Hands.AddChildrens( new List<Card>
        {
            new Card("test_1", "A", Hands, new Value()),
            new Card("test_2", "K", Hands, new Value()),
            new Card("test_3", "Q", Hands, new Value()),
            new Card("test_4", "J", Hands, new Value()),
            new Card("test_5", "10", Hands, new Value()),
            new Card("test_6", "8", Hands, new Value()),
            new Card("test_7", "5", Hands, new Value()),
            new Card("test_8", "7", Hands, new Value()),
            new Card("test_9", "9", Hands, new Value()),
        });
        _nodes.Add(Hands);
        _nodes.Add( new Table(new Vector2(Raylib.GetRenderWidth() / 2, Raylib.GetRenderHeight() * 0.5f)) );
    }
    
    public override void Update(float deltaTime)
    {
        Visuals.BackgroundColorize.Instance.BeforeDrawing();
        
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.CheckCollisionPointRec(mousePos, testDeleteRect) && !Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (DraggingCard.Instance.Card is Card)
                DraggingCard.Instance.Card.Dispose();
        }
        
        if (Raylib.CheckCollisionPointRec(mousePos, testAddRect) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            if (Hands.MaxCards > Hands.Childrens.Count)
                Hands.AddChild( new Card("test_9", "9", Hands, new Value()) );
        }
        
    }
    
    public override void Draw()
    {
        Visuals.BackgroundColorize.Instance.Draw();
        
        Raylib.DrawRectangle( (int)testDeleteRect.X, (int)testDeleteRect.Y, (int)testDeleteRect.Width, (int)testDeleteRect.Height, Color.Red);
        Raylib.DrawRectangle( (int)testAddRect.X, (int)testAddRect.Y, (int)testAddRect.Width, (int)testAddRect.Height, Color.Green);
    }
    
    public override void Dispose()
    {
        Visuals.BackgroundColorize.Instance.UnloadShader();
    }
}