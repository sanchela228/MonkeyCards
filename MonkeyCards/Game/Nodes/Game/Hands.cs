using System.Linq.Expressions;
using System.Numerics;
using Engine.Core.Objects;
using Engine.Managers;
using Game.Controllers;
using Game.Nodes.Game.Models.Card;
using Raylib_cs;

namespace Game.Nodes.Game;

public class Hands : Node
{
    protected virtual PointRendering PointRendering { get; set; } = PointRendering.Center;
    protected Font _font;
    protected bool _showCountCards = false;
    
    protected int _maxCards = 6;
    protected bool _blocked = false;

    public void Block(bool b)
    {
        _blocked = b;
        foreach (Card children in Childrens)
            children.Block(_blocked);
    }
    
    public int MaxCards => _maxCards;
    private Vector2 _centerPoint;

    public Hands(Vector2 centerPoint, float maxWidth, IEnumerable<Card> cards = null)
    {
        AddChildrens(cards);
        
        RecursiveUpdateChildren = true;
        // RecursiveDrawChildren = true;

        Position = centerPoint;
        _centerPoint = centerPoint;
        Size = new Vector2(maxWidth, 260);
        
        _font = Resources.Instance.FontEx("JockeyOne-Regular.ttf", 42);
    }
    
    protected float _hideTimer;
    protected const float HideDelay = 3f; 
    
    protected float _countCardsAlpha = 0f;
    protected const float FadeSpeed = 5f;

    public void AddCards(IEnumerable<Node> cards)
    {
        foreach (Card card in cards)
            card.SetHand(this);
        
        AddChildrens(cards);
    }

    public void AddCard(Card card)
    {
        card.SetHand(this);
        AddChild(card);
    }
    
    public override void Update(float deltaTime)
    {
        #region CounterCardView
        float targetAlpha = _showCountCards ? 1f : 0f;
    
        if (_countCardsAlpha < targetAlpha)
            _countCardsAlpha = Math.Min(_countCardsAlpha + FadeSpeed * deltaTime, 1f);
        else if (_countCardsAlpha > targetAlpha)
            _countCardsAlpha = Math.Max(_countCardsAlpha - FadeSpeed * deltaTime, 0f);
        
        if (IsMouseOverWithoutOverlap())
        {
            _showCountCards = true;
            _hideTimer = 0f;
        }
        else if (_showCountCards)
        {
            _hideTimer += deltaTime;
            if (_hideTimer >= HideDelay)
            {
                _showCountCards = false;
                _hideTimer = 0f;
            }
        }
        #endregion
        
        #region RenderCardsPositionsOnHand

        Position = _blocked ? new Vector2(Position.X, _centerPoint.Y + 65f) : _centerPoint;

        if (!Childrens.Any()) return;
        
        var count = Childrens.Count;
        var cardSize = (int) ((Card) Childrens[0]).DefaultSize.X;
        int margin = -30;
            
        int countMargins = count - 1;
        if (count > 6) margin -= count * 5;
            
        int totalWidth = (cardSize * count + countMargins * margin);
            
        Vector2[] positions = new Vector2[count];
            
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = new Vector2(
                (Position.X + ((cardSize + margin) * i)) - (totalWidth / 2) + cardSize / 2,
                Position.Y
            );
        }
            
        float spreadAmount = 0f;
        int insertIndex = -1;

        if (DraggingCard.Instance.Card is Card && IsMouseOverWithoutOverlap())
        {
            Vector2 mousePos = Raylib.GetMousePosition();

            int closestIndex = 0;
            float minDistance = Vector2.Distance(mousePos, positions[0]);
            bool isMouseOnRight = mousePos.X > positions[0].X;

            for (int i = 1; i < positions.Length; i++)
            {
                float currentDistance = Vector2.Distance(mousePos, positions[i]);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestIndex = i;
                    isMouseOnRight = mousePos.X > positions[i].X;
                }
            }
                
            DraggingCard.Instance.IndexCardOnHands = closestIndex + (isMouseOnRight ? 1 : 0);

            insertIndex = isMouseOnRight ? closestIndex + 1 : closestIndex;
            spreadAmount = cardSize * 0.5f;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            var cardRef = (Card)Childrens[i];
            bool isLastCard = i == Childrens.Count - 1;
                
            float t = 1.0f - MathF.Exp(-18f * deltaTime);
            Vector2 targetPosition = positions[i];
    
            if (spreadAmount > 0 && insertIndex >= 0)
            {
                if (i < insertIndex)
                    targetPosition.X -= spreadAmount;
                else if (i >= insertIndex)
                    targetPosition.X += spreadAmount;
            }
                
            // TODO: fix this shit
            if (count > 6 && !isLastCard )
            {
                cardRef.Collider = new Rectangle(
                    -25 + -(i * 1.5f),
                    0,
                    cardRef.DefaultSize.X - 50 + -(i * 3f),
                    cardRef.DefaultSize.Y
                );
            }
            else
            {
                cardRef.Collider = new Rectangle(
                    0,
                    0,
                    cardRef.DefaultSize.X,
                    cardRef.DefaultSize.Y
                );
            }
                
            cardRef.Position = Vector2.Lerp(cardRef.Position, targetPosition, t);
        }
        #endregion
    }
    
    public override void Draw()
    {
        if (_countCardsAlpha > 0.01f) 
        {
            Color textColor = new Color( 255, 255, 255, (int)(_countCardsAlpha * 255));

            Raylib.DrawTextPro( 
                _font, 
                Childrens.Count + " / " + MaxCards, 
                new Vector2(Position.X, Bounds.Y),
                new Vector2(21, 21),
                0f,
                42,
                3,
                textColor
            );
        }
    }

    public override void Dispose()
    {
        
    }
}