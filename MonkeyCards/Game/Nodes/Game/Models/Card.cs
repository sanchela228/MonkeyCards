using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Models;
using Engine.Core.Objects;

public class Card : Node
{
    public string Name;
    public string ShortName { get; }

    public RenderTexture2D _canvas;

    public Card(string _name)
    {
        Name = _name;
        _canvas = Raylib.LoadRenderTexture(205, 305);
        Rectangle _rect = new Rectangle(
            5,
            5,
            _canvas.Texture.Width - 5,
            _canvas.Texture.Height - 10
        );
        
        Raylib.BeginTextureMode(_canvas);
        Raylib.ClearBackground(Color.Blank);
             
        Raylib.DrawRectangleRounded(new Rectangle(
            0, 5,
            _canvas.Texture.Width,
            _canvas.Texture.Height - 5
        ), 0.2f, 10, new Color(0, 0, 0, 128));
        
        Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);
        Raylib.EndTextureMode();
    }
    
    public override void Update(float deltaTime)
    {
        
    }


    public override void Draw()
    {
        // Console.WriteLine("Draw Card", Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
        
        Raylib.DrawTexturePro(
            _canvas.Texture,
            new Rectangle(0, 0, _canvas.Texture.Width, -_canvas.Texture.Height),
            Bounds,
            new Vector2(_canvas.Texture.Width / 2, _canvas.Texture.Height / 2),
            0f,
            Color.White
        );
    }

    public override void Dispose()
    {
        
    }
}