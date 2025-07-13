using Game.Nodes.Game.Models.Card;
namespace Game.Controllers;

public class DraggingCard
{
    static DraggingCard() => Instance = new();
    public static DraggingCard Instance { get; private set; }
    
    private Card? _cardNode;

    public void Clear()
    {
        Instance._cardNode = null;
        Instance.IndexCardOnHands = null;
    }
    
    public Card Card { get => _cardNode; set => _cardNode = value; }
    public int? IndexCardOnHands;
}