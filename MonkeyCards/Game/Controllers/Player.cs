using Game.Nodes.Game;
using Game.Nodes.Game.Table;

namespace Game.Controllers;

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