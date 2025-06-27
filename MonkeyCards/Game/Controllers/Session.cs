using Game.Nodes.Game;
using Game.Nodes.Game.Models.Card;
using Game.Nodes.Game.Table;
using Game.Services;
using Raylib_cs;

namespace Game.Controllers;

public class Session
{
    public Player Self { get; protected set; }
    public Player RemotePlayer { get; protected set; }

    public int StartStack { get; protected set; } = 5;
    
    // TODO: create TIMER class
    public bool timerRunning { get; set; }
    public float RoundTime { get; protected set; }
    
    public string TextTimer { get; protected set; }

    public int Round { get; protected set; } = 1;
    
    private readonly float _roundTime = 140f;
    
    public void Init(Hands hands, Table table, IEnumerable<Card> startCards)
    {
        Self = new Player(hands)
        {
            Table = table
        };

        RemotePlayer = new Player(hands);
        Round = 1;
            
        Self.Hands.AddCards( startCards );
    }

    public async void EndRound(float money)
    {
        timerRunning = false;
        
        await AwaitTestTimer();
        
        Self.Money += money;
        
        Round++;
        
        PickToHandRoundCards();
        StartTimer();

        Self.Table?.Clear();
    }
    
    public async void EndRound()
    {
        timerRunning = false;
        
        await AwaitTestTimer();
        
        Self.Money += CardsHolder.CalcCombo(Self.Table.GetCards());
        
        Round++;

        PickToHandRoundCards();
        StartTimer();
        
        Self.Table?.Clear();
    }
    
    public async Task AwaitTestTimer()
    {
        await Task.Delay(2150); 
    }

    public void StartTimer()
    {
        timerRunning = true;
        RoundTime = _roundTime;
    }

    public void TimerUpdate(float deltaTime)
    {
        if (timerRunning && RoundTime > 0)
        {
            RoundTime -= deltaTime;

            int minutes = (int)(RoundTime / 60);
            int seconds = (int)(RoundTime % 60);

            TextTimer = $"{minutes:D2}:{seconds:D2}";
        }
        else if (timerRunning && RoundTime <= 0)
        {
            EndRound();
        }
    }

    public void PickToHandRoundCards()
    {
        if ( Self.Hands.CountCards >= Self.Hands.MaxCards) 
            return;
        
        if (Self.Hands.CountCards <= Self.Hands.MaxCards)
        {
            if ( (Self.Hands.MaxCards - Self.Hands.CountCards) == 1 )
                Self.Hands.AddCard( CardsHolder.Instance.Defaults.Pop() );
            else
            {
                Self.Hands.AddCards( CardsHolder.Instance.TakeFromTop(2) );
            }
        }
    }

    public void PickSpecialCards(int count)
    {
        if ( Self.Hands.CountCards >= Self.Hands.MaxCards) 
            return;
        
        Self.Hands.AddCards( CardsHolder.Instance.TakeSpecials(count) );
    }
    

    public void SellCard(Card card)
    {
        Self.Money += MathF.Round(card.Cost / 2, 1);
        
        card.BurnCard();
    }
    
    static Session() => Instance = new();
    public static Session Instance { get; private set; }
}