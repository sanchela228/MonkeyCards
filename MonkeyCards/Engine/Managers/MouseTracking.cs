using MonkeyCards.Engine.Core.Objects;

namespace MonkeyCards.Engine.Managers;

public class MouseTracking
{
    static MouseTracking() => Instance = new();
    public static MouseTracking Instance { get; private set; }
    public Node HoveredNode { get; set; }
    
    public bool BlockedHover { get; set; }
}