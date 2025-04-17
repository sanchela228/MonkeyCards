namespace MonkeyCards.Game.Services;

public class CardsHolder
{  
    
    
    
    
    static CardsHolder() => Instance = new();
    public static CardsHolder Instance { get; private set; }
}