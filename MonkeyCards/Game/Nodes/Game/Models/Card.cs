using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Game.Nodes.Game.Models;
using Engine.Core.Objects;

public class Card : Node
{
    public string Name;
    public string ShortName { get; }

    public RenderTexture2D _canvas;

    protected override PointRendering PointRendering { get; set; } = PointRendering.LeftTop;

    public Card(string _name)
    {
        Name = _name;

        Size = new Vector2(100, 100);
        
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
        Raylib.DrawTexturePro(
            _canvas.Texture,
            new Rectangle(0, 0, _canvas.Texture.Width, -_canvas.Texture.Height),
            new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height),
            Vector2.Zero,
            Rotation,
            Color.White
        );
    }

    public override void Dispose()
    {
        
    }
}