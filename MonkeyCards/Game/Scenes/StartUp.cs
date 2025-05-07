using System.Numerics;
using Engine.Core;
using Engine.Core.Scenes;
using Engine.Helpers;
using Engine.Managers;
using Game.Services.Network;
using Game.Visuals;
using Raylib_cs;
using Rectangle = Raylib_cs.Rectangle;

namespace Game.Scenes;

public class StartUp : Scene
{
    private FontFamily f;
    private FontFamily fm;
    private Texture2D t;
    private Vector2 centerScreen;
    
    public Animator Animator = new();

    private Authorization _authorizator = new();
    
    public StartUp()
    {
        t = Resources.Instance.Texture("Images/MonkeyPreview.png");
        f = new FontFamily()
        {
            Font = Resources.Instance.FontEx("JandaManateeSolidCyrillic.ttf", 58),
            Size = 58,
            Spacing = 6,
            Color = Color.White
        };
        
        fm = new FontFamily()
        {
            Font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 34),
            Size = 34,
            Spacing = 2,
            Color = Color.White
        };
        
        Animator.Task((progress) =>
        {
            Color color = f.Color;
            color.A = (byte)(progress * 255);


            Text.DrawPro(f, "MonkeySpeak Team", centerScreen, color: color);
            Texture.DrawEx(t, new Vector2(centerScreen.X, centerScreen.Y - 120), color: color);

        }, duration: 2f, mirror: true, removable: true);

        Animator.Task((progress) =>
        {
            Color color = f.Color;
            color.A = (byte)(progress * 255);
            
            Text.DrawPro(f, "A Game With Cards", centerScreen, color: color);
        }, 
        onComplete: () =>
        {
            Animator.Task((progress) =>
            {
                Raylib.DrawRectanglePro( 
                    new Rectangle(centerScreen.X, centerScreen.Y + 150, 60, 60), 
                    new Vector2(30, 30), 
                    progress * 360f, 
                    new Color(255,255,255)
                );
                
                Text.DrawPro(f, "A Game With Cards", centerScreen, color: Color.White);
                
                Text.DrawPro(fm, _authorizator.Status, new Vector2(centerScreen.X, centerScreen.Y + 230), color: Color.White);
                
            }, duration: 2f, repeat: true);
            
            Console.WriteLine("Auth entrance");
            
            _authorizator.Entrance();

        }, duration: 0.2f, delay: 5f);

    }
    
    public override void Update(float deltaTime)
    {
        centerScreen = new Vector2(Raylib.GetScreenWidth() / 2, (Raylib.GetScreenHeight() + 30) / 2);
        
        Animator.Update(deltaTime);

        if (_authorizator.IsAuthorized)
        {
            Manager.Instance.PushScene( new Menu() );
        }
    }

    public override void Draw()
    {
        Raylib.ClearBackground( new Color(25, 25, 25));
       
        
        Animator.Draw();
        
        
        
       

    }

    public override void Dispose()
    {
        
    }
}