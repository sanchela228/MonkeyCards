using System.Text.Json.Serialization;
using Engine.Core;
using Game.Helpers.Converters;
using Game.Nodes.Game.Models.Card;

namespace Game.Presentation;

public class JsonCardResource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Symbol { get; set; }
    public CardSuit Suit { get; set; }
    public float Cost { get; set; }
    public int Multiply { get; set; }
    public Border Border { get; set; }
    
    public BackgroundType Background { get; set; }
    
    [JsonConverter(typeof(FontFamilyConverter))]
    public FontFamily FontFamily { get; set; }
    
    [JsonConverter(typeof(ViewConverter))]
    public View View { get; set; }
    
    [JsonConverter(typeof(EffectConverter))]
    public Effect? Effect { get; set; }
    
    [JsonConverter(typeof(SpecialConverter))]
    public Special? Special { get; set; }
}