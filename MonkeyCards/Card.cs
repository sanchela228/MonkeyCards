namespace MonkeyCards;

public abstract class Card
{
    public string Name { get; }
    public string ShortName { get; }
    protected Card(string name, string shortName)
    {
        Name = name;
        ShortName = shortName;
    }
}