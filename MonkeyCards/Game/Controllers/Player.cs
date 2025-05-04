using Game.Nodes.Game;

namespace Game.Controllers;

public class Player
{
    public float Money { get; set; } = 0f;
    public Hands Hands { get; set; }

    public Player(Hands hands)
    {
        Hands = hands;
    }
}