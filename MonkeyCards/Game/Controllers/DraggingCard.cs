using MonkeyCards.Game.Nodes.Game.Models;
namespace MonkeyCards.Game.Controllers;

public class DraggingCard
{
    static DraggingCard() => Instance = new();
    public static DraggingCard Instance { get; private set; }
    
    private Card _cardNode = null;
    
    public Card Card { get => _cardNode; set => _cardNode = value; }
    public int? IndexCardOnHands;
}