using System.Text.Json;
using System.Text.Json.Serialization;
using ResourceManager = MonkeyCards.Engine.Managers.Resources;
using MonkeyCards.Game.Nodes.Game.Models.Card;
using MonkeyCards.Game.Presentation;

namespace MonkeyCards.Game.Services;

public class CardsHolder
{
    public Stack<Card> Defaults { get; set; } = new();

    public List<Card> TakeFromTop(int count)
    {
        List<Card> cards = new();

        for (int i = 0; i < count; i++)
        {
            if (Defaults.Count > 0)
                cards.Add( Defaults.Pop() );
        }
        
        return cards;
    }
    
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
        
        var suitGroups = new Dictionary<string, List<Card>>();
        
        foreach (var card in cards)
        {
            if ( !suitGroups.ContainsKey(card.Symbol) )
                suitGroups[card.Symbol] = new();
            
            suitGroups[card.Symbol].Add(card);
        }

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

            var localCost = x.Cost;
            
            if (suitGroups.ContainsKey(x.Symbol) && suitGroups[x.Symbol].Count > 1)
                localCost *= 1.5f;
  
            return localCost * x.Multiply * borderMultiple * backgroundMultiple;
        });
        
        int redColorCount = 0;
        int blackColorCount = 0;
        
        foreach (var card in cards)
        {
            if (IsRedSuit(card.Suit))
                redColorCount++;
            
            if (IsBlackSuit(card.Suit))
                blackColorCount++;
        }
        
        if (redColorCount > 4 || blackColorCount > 4)
            cost *= 2f;
    
        return MathF.Round(cost, 1);
    }
    
    private static bool IsRedSuit(CardSuit suit)
    {
        return suit == CardSuit.Hearts || suit == CardSuit.Diamonds;
    }
    private static bool IsBlackSuit(CardSuit suit)
    {
        return suit == CardSuit.Clubs || suit == CardSuit.Spades;
    }
    
    
    static CardsHolder() => Instance = new();
    public static CardsHolder Instance { get; private set; }
}