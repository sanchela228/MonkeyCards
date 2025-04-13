using System.Numerics;
using Raylib_cs;

namespace MonkeyCards.Engine.Core.Objects;

public enum PointRendering
{
    Center,
    LeftTop,
}

public abstract class Node : IDisposable
{
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void Dispose();
    
    public bool IsActive { get; set; } = true;
    public int Order = 100;
    
    public void RootUpdate(float deltaTime)
    {
        Update(deltaTime);

        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens) 
                child.RootUpdate(deltaTime);
        }
    }
    
    public void RootDraw()
    {
        Draw();
        
        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens) 
                child.Draw();
        }
    }
    
    public void RootDispose()
    {
        Dispose();
        
        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens) 
                child.Dispose();
        }
    }
    
    private Vector2 _position;
    
    public Vector2 Position 
    { 
        get 
        {
            if (_parent == null)
                return _position;
            return _parent.Position + _position;
        }
        set 
        {
            if (_parent == null)
                _position = value;
            else
                _position = value - _parent.Position;
        }
    }
    
    private float _rotation = 0f;
    public float Rotation 
    { 
        get => _parent == null ? _rotation : _parent.Rotation + _rotation;
        set 
        {
            if (_parent == null)
                _rotation = value;
            else
                _rotation = value - _parent.Rotation;
        }
    }

    private Node _parent = null;
    protected List<Node> _childrens = new();

    protected virtual PointRendering PointRendering { get; set; } = PointRendering.Center;
    
    public Node Parent
    {
        get => _parent;
    }
    
    public void SetParent(Node parent) => _parent = parent;
    
    public List<Node> Childrens
    {
        get => _childrens;
    }

    public void AddChild(Node node)
    {
        _childrens.Add(node);
        node.SetParent(this);
    }
    
    public void AddChildrens(IEnumerable<Node> list)
    {
        if (list is not null && list.Any())
        {
            _childrens.AddRange(list);
            
            foreach (var node in list) 
                node.SetParent(this);
        }
    }

    public void RemoveChild(Node node)
    {
        _childrens.Remove(node);
        node.SetParent(null);
    }
    
    private Vector2 _size;
    private Vector2 _scale = Vector2.One;
    private Rectangle _bounds => new Rectangle(Position.X, Position.Y, Size.X, Size.Y);

    public Rectangle Bounds
    {
        get
        {
            if (PointRendering == PointRendering.Center)
                return Helpers.Rectangle.CenterShiftRec(Position, Size);
        
            return _bounds;
        }
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