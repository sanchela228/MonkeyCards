namespace MonkeyCards;

public class Hands
{
    readonly List<Card> _cards = new();

    public void addCard(Card card) => _cards.Add(card);
    public int count() => _cards.Count;
}