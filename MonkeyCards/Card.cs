using Raylib_cs;

namespace MonkeyCards;

public abstract class Card
{
    public string Name { get; }
    public string ShortName { get; }

    public Rectangle _rect;
    
    protected Card(string name, string shortName)
    {
        Name = name;
        ShortName = shortName;
        
         _rect = new Rectangle(
             400,
             100,
             240,
             400
         );
    }

    public void Draw()
    {
        Raylib.DrawRectangleRounded(_rect, 0.2f, 10, Color.White);
        Raylib.DrawRectangleRoundedLinesEx(_rect, 0.2f, 10, 4,  Color.Black);
    }
}