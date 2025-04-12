using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Engine.Core.Objects;

public abstract class Node : IDisposable
{
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void Dispose();
    public Vector2 Position { get; set; }
    public float Rotation { get; set; } = 0f;
    public bool IsActive { get; set; } = true;
    public int Order = 100;
    
    private Vector2 _size;
    private Vector2 _scale = Vector2.One;
    private Rectangle _bounds => new Rectangle(Position.X, Position.Y, Size.X, Size.Y);

    public Rectangle Bounds
    {
        get => _bounds;
    }
    public Vector2 Size 
    { 
        get => new Vector2(_size.X * _scale.X, _size.Y * _scale.Y);
        set => _size = value;
    }
    public Vector2 Scale
    { 
        get => _scale; 
        set => _scale = value; 
    }
    public bool ICollisionWith(Rectangle rect) => Raylib.CheckCollisionRecs(rect, Bounds);
    public virtual bool IsMouseOver()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, Bounds);
    }
    public virtual bool IsMousePressed() => IsMouseOver() && Raylib.IsMouseButtonPressed(MouseButton.Left);
}