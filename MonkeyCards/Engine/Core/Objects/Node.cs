using System.Numerics;
using MonkeyCards.Engine.Core.Scenes;
using MonkeyCards.Engine.Managers;
using Raylib_cs;
using SceneManager = MonkeyCards.Engine.Core.Scenes.Manager;

namespace MonkeyCards.Engine.Core.Objects;

public enum PointRendering
{
    Center,
    LeftTop,
}

public enum OverlapsMode
{
    None,
    Exclusive
}

public abstract class Node : IDisposable
{
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void Dispose();
    
    public bool IsActive { get; set; } = true;
    public int Order = 100;
    
    protected OverlapsMode Overlap { get; set; } = OverlapsMode.Exclusive;
    
    public void RootUpdate(float deltaTime)
    {
        Update(deltaTime);

        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens.OrderBy(node => node.Order).ToList()) 
                child.RootUpdate(deltaTime);
        }
    }
    
    public void RootDraw()
    {
        Draw();

        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens.OrderBy(node => node.Order).ToList()) 
                child.RootDraw();
        }
    }
    
    public void RootDispose()
    {
        Dispose();
        
        if (_childrens is not null && _childrens.Any())
        {
            foreach (var child in _childrens.OrderBy(node => node.Order).ToList()) 
                child.RootDispose();
        }
    }
    
    private Vector2 _position;
    
    public Vector2 Position 
    { 
        get 
        {
            if (_parent == null) return _position;
            return _parent.Position + _position;
        }
        set 
        {
            if (_parent == null) _position = value;
            else _position = value - _parent.Position;
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

    public void SetParent(Node newParent, int index = -1, Vector2? position = null)
    {
        if (_parent == newParent)
            return;

        if (_parent != null)
            _parent._childrens.Remove(this);

        _parent = newParent;
        
        if (position is not null)
            Position = position ?? Vector2.Zero;
        
        if (SceneManager.Instance.PeekScene() is not null && SceneManager.Instance.PeekScene().ContainsNode(this))
            SceneManager.Instance.PeekScene().RemoveNode(this);

        if (_parent != null && !_parent._childrens.Contains(this))
        {
            if (index == -1)
                _parent._childrens.Add(this);
            else
                _parent._childrens.Insert(index, this);
        }
    }
    
    public void SetParent(Scene scene)
    {
        if (_parent != null)
            _parent._childrens.Remove(this);

        _parent = null;
        
        scene.AddNode(this);
    }
    
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
        if (MouseTracking.Instance.BlockedHover && MouseTracking.Instance.HoveredNode != this)
            return false;
        
        Vector2 mousePos = Raylib.GetMousePosition();

        if (!Raylib.CheckCollisionPointRec(mousePos, Bounds))
        {
            if (MouseTracking.Instance.HoveredNode == this)
                MouseTracking.Instance.HoveredNode = null;
            
            return false;
        }

        if (Overlap == OverlapsMode.Exclusive)
        {
            if (MouseTracking.Instance.HoveredNode != null &&
                MouseTracking.Instance.HoveredNode != this)
            {
                return false;
            }

            MouseTracking.Instance.HoveredNode = this;
        }

        return true;
    }
    
    public virtual bool IsMouseOverWithoutOverlap()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        
        return Raylib.CheckCollisionPointRec(mousePos, Bounds);
    }
    public virtual bool IsMousePressed() => IsMouseOver() && Raylib.IsMouseButtonPressed(MouseButton.Left);
}