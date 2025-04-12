namespace MonkeyCards.Engine.Core.Scenes;

using Objects;
public abstract class Scene
{
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void Dispose();
    
    protected List<Node> _nodes = new();

    protected Scene()
    {
        if (this._nodes.Count > 1)
            _nodes.Sort((node1, node2) => node1.Order.CompareTo(node2.Order));
    }

    public void RootUpdate(float deltaTime)
    {
        Update(deltaTime);
        
        NodesUpdate(deltaTime);
    }
    
    public void RootDraw()
    {
        Draw();
        
        NodesDraw();
    }
    
    public void RootDispose()
    {
        if (this._nodes.Count > 0)
        {
            foreach (var node in _nodes) node.Dispose();
        }
        
        _nodes.Clear();
        Dispose();
    }

    private void NodesUpdate(float deltaTime)
    {
        foreach (var node in _nodes)
        {
            if (node.IsActive) node.Update(deltaTime);
        }
    }
    
    private void NodesDraw()
    {
        foreach (var node in _nodes)
        {
            if (node.IsActive) node.Draw();
        }
    }
}