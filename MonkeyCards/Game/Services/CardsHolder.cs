using System.Text.Json;
using System.Text.Json.Serialization;
using ResourceManager = MonkeyCards.Engine.Managers.Resources;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Presentation;

namespace MonkeyCards.Game.Services;

public class CardsHolder
{
    public Stack<Card> Defaults { get; set; } = new();

    public void LoadCards()
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };

        string json = ResourceManager.Instance.Get<string>("Cards/defaults.json");
        
        JsonCardResource[] resources = JsonSerializer.Deserialize<JsonCardResource[]>(json, options);

        List<Card> list = new();
        foreach (JsonCardResource cardResource in resources)
        {
            list.Add( new Card(
                cardResource.Id,
                cardResource.Name,
                cardResource.Symbol,
                cardResource.Suit,
                cardResource.Cost,
                cardResource.FontFamily,
                cardResource.View,
                cardResource.Description,
                cardResource.Multiply,
                cardResource.Border,
                cardResource.Effect )
            );
        }

        list.OrderBy(x => Random.Shared.Next()).ToList().ForEach(card => Defaults.Push(card));
    }
    
    static CardsHolder() => Instance = new();
    public static CardsHolder Instance { get; private set; }
}