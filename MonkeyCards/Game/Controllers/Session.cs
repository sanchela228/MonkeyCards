using Game.Nodes.Game;
using Game.Nodes.Game.Models.Card;

namespace Game.Controllers;

public class Session
{
    public Player Self { get; protected set; }
    public Player RemotePlayer { get; protected set; }

    private int StartStack = 5;
    
    public void Init(Hands hands, IEnumerable<Card> startCards)
    {
        Self = new Player(hands);
        
        Self.Hands.AddChildrens( startCards );
    }
    
    
    static Session() => Instance = new();
    public static Session Instance { get; private set; }
}