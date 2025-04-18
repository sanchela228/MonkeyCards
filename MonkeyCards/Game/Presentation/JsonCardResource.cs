using System.Text.Json.Serialization;
using MonkeyCards.Game.Helpers.Converters;
using MonkeyCards.Game.Nodes.Game.Models.Card;

namespace MonkeyCards.Game.Presentation;

public class JsonCardResource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Char Symbol { get; set; }
    public CardSuit Suit { get; set; }
    public float Cost { get; set; }
    public float Multiply { get; set; }
    public Border Border { get; set; }
    
    [JsonConverter(typeof(FontFamilyConverter))]
    public FontFamily FontFamily { get; set; }
    
    [JsonConverter(typeof(ViewConverter))]
    public View View { get; set; }
    
    [JsonConverter(typeof(EffectConverter))]
    public Effect? Effect { get; set; }
}