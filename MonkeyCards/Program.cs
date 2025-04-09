
using System;
using System.Numerics;
using Raylib_cs;


// Инициализация
Raylib.InitWindow(800, 600, "Карточная игра");
Raylib.SetTargetFPS(60);



// Создаем карты
int cardCount = 4;
Card[] cards = new Card[cardCount];

float cardWidth = 80;
float cardHeight = 120;
float spacing = 4;
float totalWidth = cardCount * cardWidth + (cardCount - 1) * spacing;
float startX = (Raylib.GetScreenWidth() - totalWidth) / 2;
float yPos = Raylib.GetScreenHeight() - cardHeight - 20;

for (int i = 0; i < cardCount; i++)
{
    cards[i] = new Card
    {
        Rect = new Rectangle(startX + i * (cardWidth + spacing), yPos, cardWidth, cardHeight),
        Color = Color.Gold,
        Text = "Card " + (i + 1)
    };
}

int hoveredCard = -1;
float animationSpeed = 1f;

while (!Raylib.WindowShouldClose())
{
    // Обновление
    Vector2 mousePos = Raylib.GetMousePosition();
    hoveredCard = -1;

    // Проверка наведения и анимация
    for (int i = 0; i < cardCount; i++)
    {
        // Проверяем наведение (с учетом масштаба)
        Rectangle scaledRect = new Rectangle(
            cards[i].Rect.X - (cards[i].Rect.Width * (cards[i].Scale - 1)) / 2,
            cards[i].Rect.Y - (cards[i].Rect.Height * (cards[i].Scale - 1)),
            cards[i].Rect.Width * cards[i].Scale,
            cards[i].Rect.Height * cards[i].Scale
        );

        if (Raylib.CheckCollisionPointRec(mousePos, scaledRect))
        {
            hoveredCard = i;
            cards[i].TargetScale = 1.3f;
        }
        else
        {
            cards[i].TargetScale = 1.0f;
        }

        // Плавное изменение масштаба
        cards[i].Scale = Lerp(cards[i].Scale, cards[i].TargetScale, animationSpeed);
    }

    // Отрисовка
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);

    // Рисуем карты
    for (int i = 0; i < cardCount; i++)
    {
        // Вычисляем смещение для эффекта разъезжания
        float spreadOffset = 0;
        if (hoveredCard >= 0)
        {
            float direction = Math.Sign(i - hoveredCard);
            float distance = Math.Abs(i - hoveredCard);
            spreadOffset = direction * 20 * (1 - (float)Math.Exp(-distance * 0.5));
        }

        // Позиция с учетом масштаба и смещения
        Vector2 position = new Vector2(
            cards[i].Rect.X + spreadOffset - (cards[i].Rect.Width * (cards[i].Scale - 1)) / 2,
            cards[i].Rect.Y - (cards[i].Rect.Height * (cards[i].Scale - 1))
        );

        // Отрисовка карты
        Raylib.DrawRectangleRounded(
            new Rectangle(position.X, position.Y, 
                         cards[i].Rect.Width * cards[i].Scale, 
                         cards[i].Rect.Height * cards[i].Scale),
            0.1f, 10, cards[i].Color);

        // Текст на карте
        Raylib.DrawText(cards[i].Text, 
            (int)(position.X + 10 * cards[i].Scale), 
            (int)(position.Y + 50 * cards[i].Scale), 
            (int)(20 * cards[i].Scale), 
            Color.Black);
    }

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
float Lerp(float a, float b, float t)
{
    return a + (b - a) * t;
}
public class Card
{
    public Rectangle Rect;
    public Color Color;
    public string Text;
    public float Scale = 1.0f;
    public float TargetScale = 1.0f;
    public float Rotation = 0.0f;
}
