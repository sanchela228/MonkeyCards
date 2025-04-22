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
                cardResource.Background,
                cardResource.Effect )
            );
        }

        list.OrderBy(x => Random.Shared.Next()).ToList().ForEach(card => Defaults.Push(card));
    }

    public static float CalcCombo(IEnumerable<Card> cards)
    {
        float cost = 0f;
    
        var colorGroups = cards.GroupBy(x => IsRedSuit(x.Suit)).ToDictionary(g => g.Key, g => g.Count());
    
        cost += cards.Sum(x =>
        {
            float borderMultiple = 1f;
            float backgroundMultiple = 1f;
            float comboBonus = 1f;
        
            borderMultiple = x.Border switch
            {
                Border.Gold => borderMultiple + 0.15f,
                _ => borderMultiple
            };

            backgroundMultiple = x.Background switch
            {
                BackgroundType.Gold => backgroundMultiple + 0.2f,
                _ => backgroundMultiple
            };
        
            if (cards.Count() > 1)
                comboBonus += 0.4f * cards.Count();
        
            bool isRed = IsRedSuit(x.Suit);
            int sameColorCount = colorGroups.GetValueOrDefault(isRed, 0);
        
            if (sameColorCount > 1)
                comboBonus += 0.2f * sameColorCount;
            
            return x.Cost * x.Multiply * borderMultiple * backgroundMultiple * comboBonus;
        });
    
        return MathF.Round(cost, 1);
    }
    
    private static bool IsRedSuit(CardSuit suit)
    {
        return suit == CardSuit.Hearts || suit == CardSuit.Diamonds;
    }
    
    
    static CardsHolder() => Instance = new();
    public static CardsHolder Instance { get; private set; }
}