using MonkeyCards.Game.Nodes.Game;
using MonkeyCards.Game.Nodes.Game.Table;

namespace MonkeyCards.Game.Controllers;

public class Player
{
    public float Money { get; set; } = 0f;
    public Hands Hands { get; set; }
    public Table? Table { get; set; }

    public Player(Hands hands)
    {
        Hands = hands;
    }
}