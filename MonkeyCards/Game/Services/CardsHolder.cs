using System.Text.Json;
using System.Text.Json.Serialization;
using MonkeyCards.Engine.Managers;
using MonkeyCards.Game.Helpers;
using MonkeyCards.Game.Nodes.Game.Models.Card;

namespace MonkeyCards.Game.Services;

class Test()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Char Symbol { get; set; }
    public CardSuit Suit { get; set; }
    public float Cost { get; set; }
    public int Multiply { get; set; }
    public Border Border { get; set; }
    
    [JsonConverter(typeof(FontFamilyConverter))]
    public FontFamily FontFamily { get; set; }
    
    [JsonConverter(typeof(ViewConverter))]
    public View View { get; set; }
    
    [JsonConverter(typeof(EffectConverter))]
    public Effect? Effect { get; set; }
}

public class CardsHolder
{
    public void LoadCards()
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        string json = Resources.Instance.Get<string>("cards.json");

        
        Test[] card = JsonSerializer.Deserialize<Test[]>(json, options);

        Console.WriteLine(card);
    }
    
    static CardsHolder() => Instance = new();
    public static CardsHolder Instance { get; private set; }
}