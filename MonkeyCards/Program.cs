using MonkeyCards.Engine.Core;
using MonkeyCards.Game.Scenes;

using var game = new Game( new MonkeyCards.Engine.Configuration.Game() );
game.Run( new Menu() );





// Raylib.InitWindow(800, 600, "Карточная игра");
// Raylib.SetTargetFPS(60);
//
//
//
// int cardCount = 4;
// Card[] cards = new Card[cardCount];
//
// float cardWidth = 80;
// float cardHeight = 120;
// float spacing = 4;
// float totalWidth = cardCount * cardWidth + (cardCount - 1) * spacing;
// float startX = (Raylib.GetScreenWidth() - totalWidth) / 2;
// float yPos = Raylib.GetScreenHeight() - cardHeight - 20;
//
// for (int i = 0; i < cardCount; i++)
// {
//     cards[i] = new Card
//     {
//         Rect = new Rectangle(startX + i * (cardWidth + spacing), yPos, cardWidth, cardHeight),
//         Color = Color.Gold,
//         Text = "Card " + (i + 1)
//     };
// }
//
// int hoveredCard = -1;
// float animationSpeed = 1f;
//
// while (!Raylib.WindowShouldClose())
// {
//     Vector2 mousePos = Raylib.GetMousePosition();
//     hoveredCard = -1;
//
//     for (int i = 0; i < cardCount; i++)
//     {
//         Rectangle scaledRect = new Rectangle(
//             cards[i].Rect.X - (cards[i].Rect.Width * (cards[i].Scale - 1)) / 2,
//             cards[i].Rect.Y - (cards[i].Rect.Height * (cards[i].Scale - 1)),
//             cards[i].Rect.Width * cards[i].Scale,
//             cards[i].Rect.Height * cards[i].Scale
//         );
//
//         if (Raylib.CheckCollisionPointRec(mousePos, scaledRect))
//         {
//             hoveredCard = i;
//             cards[i].TargetScale = 1.3f;
//         }
//         else
//         {
//             cards[i].TargetScale = 1.0f;
//         }
//
//         cards[i].Scale = Lerp(cards[i].Scale, cards[i].TargetScale, animationSpeed);
//     }
//
//     Raylib.BeginDrawing();
//     Raylib.ClearBackground(Color.White);
//
//     for (int i = 0; i < cardCount; i++)
//     {
//         float spreadOffset = 0;
//         if (hoveredCard >= 0)
//         {
//             float direction = Math.Sign(i - hoveredCard);
//             float distance = Math.Abs(i - hoveredCard);
//             spreadOffset = direction * 20 * (1 - (float)Math.Exp(-distance * 0.5));
//         }
//
//         Vector2 position = new Vector2(
//             cards[i].Rect.X + spreadOffset - (cards[i].Rect.Width * (cards[i].Scale - 1)) / 2,
//             cards[i].Rect.Y - (cards[i].Rect.Height * (cards[i].Scale - 1))
//         );
//
//         Raylib.DrawRectangleRounded(
//             new Rectangle(position.X, position.Y, 
//                          cards[i].Rect.Width * cards[i].Scale, 
//                          cards[i].Rect.Height * cards[i].Scale),
//             0.1f, 10, cards[i].Color);
//
//         Raylib.DrawText(cards[i].Text, 
//             (int)(position.X + 10 * cards[i].Scale), 
//             (int)(position.Y + 50 * cards[i].Scale), 
//             (int)(20 * cards[i].Scale), 
//             Color.Black);
//     }
//
//     Raylib.EndDrawing();
// }
//
// Raylib.CloseWindow();
// float Lerp(float a, float b, float t)
// {
//     return a + (b - a) * t;
// }
// public class Card
// {
//     public Rectangle Rect;
//     public Color Color;
//     public string Text;
//     public float Scale = 1.0f;
//     public float TargetScale = 1.0f;
//     public float Rotation = 0.0f;
// }
