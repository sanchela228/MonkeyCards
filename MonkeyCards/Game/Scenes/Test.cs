using System.Numerics;
using Engine.Core.Scenes;
using Engine.Managers;
using Game.Controllers;
using Game.Nodes.Game;
using Game.Nodes.Game.Models.Card;
using Game.Nodes.Game.Table;
using Game.Services;
using Raylib_cs;

namespace Game.Scenes;

public class Test : Scene
{
    private Rectangle exitButton;

    private Font _font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42);
    
    public readonly Hands Hands = new( 
        new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.85f), Raylib.GetScreenWidth() * 0.8f 
    );

    public readonly Table Table = new Table(new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.5f));
    
    public Rectangle testDeleteRect = new(30, 30, 100, 100);
    public Rectangle testAddRect = new(230, 30, 100, 100);
    
    public Rectangle testAddSpecial = new(480, 30, 100, 100);
    
    public Rectangle endRoundButtonTest = new(720, 30, 100, 100);
    
    
    public Test()
    {
        Console.WriteLine("START TEST SCENE");
        
        Visuals.BackgroundColorize.Instance.SetSettings();
        CardsHolder.Instance.LoadCards();
        
        exitButton = new Rectangle(100, 240, 200, 50);
        
        Session.Instance.Init(Hands, Table,  CardsHolder.Instance.TakeFromTop( Session.Instance.StartStack ));
        Session.Instance.StartTimer();
        
        // TODO: ADD CARDSHOLDER VIEW AND COUNTER
        
        AddNode(Hands);
        AddNode(Table);
        AddNode(new SessionPlayerStatus( new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.2f)));
    }
    
    protected override void Update(float deltaTime)
    {
        Visuals.BackgroundColorize.Instance.BeforeDrawing();
        
        Session.Instance.TimerUpdate(deltaTime);
        
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.CheckCollisionPointRec(mousePos, testDeleteRect) && !Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (DraggingCard.Instance.Card is Card && !DraggingCard.Instance.Card.IsOnBurningAnimation)
                Session.Instance.SellCard( DraggingCard.Instance.Card );
        }
        
        if (Raylib.CheckCollisionPointRec(mousePos, testAddRect) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            if (Hands.MaxCards > Hands.ChildrenCount && CardsHolder.Instance.Defaults.Any())
                Hands.AddCard(CardsHolder.Instance.Defaults.Pop());
        }
        
        if (Raylib.CheckCollisionPointRec(mousePos, testAddSpecial) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            if (Hands.MaxCards > Hands.ChildrenCount && CardsHolder.Instance.Specials.Any())
                Hands.AddCard(CardsHolder.Instance.Specials.Pop());
        }
        
        if (Raylib.CheckCollisionPointRec(mousePos, endRoundButtonTest) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
           List<Card> cards = Table.GetCards();
           
           Session.Instance.EndRound( CardsHolder.CalcCombo(cards) );
           
           Table.Clear();
        }
        
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) &&
            Raylib.CheckCollisionPointRec(mousePos, exitButton))
        {
            if (Manager.Instance.HasPreviousScene()) Manager.Instance.PopScene();
            else Raylib.CloseWindow();
        }
    }
    
    protected override void Draw()
    {
        Visuals.BackgroundColorize.Instance.Draw();
        
        Raylib.DrawTextPro( 
            _font, 
            Session.Instance.TextTimer + " Round:" + Session.Instance.Round, 
            new Vector2(200, 200),
            new Vector2(21, 21),
            0f,
            42,
            3,
            Color.Black
        );
        
        Raylib.DrawRectangle( (int)testDeleteRect.X, (int)testDeleteRect.Y, (int)testDeleteRect.Width, (int)testDeleteRect.Height, Color.Red);
        Raylib.DrawRectangle( (int)testAddRect.X, (int)testAddRect.Y, (int)testAddRect.Width, (int)testAddRect.Height, Color.Green);
        Raylib.DrawRectangle( (int)endRoundButtonTest.X, (int)endRoundButtonTest.Y, (int)endRoundButtonTest.Width, (int)endRoundButtonTest.Height, Color.Yellow);
        Raylib.DrawRectangle( (int)testAddSpecial.X, (int)testAddSpecial.Y, (int)testAddSpecial.Width, (int)testAddSpecial.Height, Color.Blue);
        
        Raylib.DrawRectangleRec(exitButton, Color.Red);
        Raylib.DrawText("Exit", (int)exitButton.X + 70, (int)exitButton.Y + 15, 20, Color.White);
    }
    
    protected override void Dispose()
    {
        Console.WriteLine("END TEST SCENE");
        
        
        Visuals.BackgroundColorize.Instance.UnloadShader();
    }
}