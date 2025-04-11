using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Engine.Core.Objects;

public abstract class Node : IDisposable
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Rectangle Bounds => new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
    public bool IsActive { get; set; } = true;

    public int Order = 100;
    
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void Dispose();
    public virtual bool IsMouseOver()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        return Raylib.CheckCollisionPointRec(mousePos, Bounds);
    }
        
    public virtual bool IsMouseClicked()
    {
        return IsMouseOver() && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }
    
}