using System.Numerics;
using Raylib_cs;

namespace Game.Nodes.Game.Models.Card;

public struct View
{
    public View()
    {
        _positions = new List<Vector2>();
        Size = new List<Vector2>();
        Rotate = new List<float>();
    }
    public Texture2D Texture { get; set; }
    private List<Vector2> _positions;
    public List<Vector2> Positions { get => _positions; }
    public void AddPosition(Vector2 position) => _positions.Add( Vector2.Clamp(position, Vector2.Zero, Vector2.One) );
    public Vector2 Position
    {
        get => _positions.Count > 0 ? _positions[0] : Vector2.Zero;
        set
        {
            if (_positions.Count == 0)
                _positions.Add(value);
            else
                _positions[0] = value;
        }
    }

    public List<Vector2> Size { get; set; }
    public List<float> Rotate { get; set; }
    
    public bool Sides { get; set; }
    public bool ReversText { get; set; }
    public Color Color { get; set; }
}