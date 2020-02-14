using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Program
{
    internal class Game
    {
        private const ushort Height = 900;
        private const ushort Width = 1800;
        private const byte FPSLimit = 120;

        private byte score;
        private string ScoreText = "0";
        private ushort BallSize = 50;

        private static float BatHeight = 300f;
        private static float BatWidth = 50f;

        private float timeStep = 1f / FPSLimit;


        static float BatPosY = Height - BatHeight * 2;
        static float BatTwoPosX = Width - BatWidth;

        CircleShape Ball;
        RenderWindow window;
        RectangleShape LeftBat;
        RectangleShape RightBat;

        private bool Key_W_Is_Pressed;
        private bool Key_S_Is_Pressed;
        private bool Key_ArrowUp_Is_Pressed;
        private bool Key_ArrowDown_Is_Pressed;

        private bool IsEndGame;

        private Vector2f Velocity;
        private Vector2f SpeedOfBats;

      /*  private void Score()
        {
            if (CollisonLogic() == 1)
                score++;
            Font font;
            Text text(ScoreText, font); 
        }*/

        private void LoseMechanic()
        {
            if(Ball.Position.X== 0 || Ball.Position.X == Width)
                IsEndGame = true;    
        }


        public void Init()
        {
            window = new RenderWindow(new VideoMode(Width, Height), "Ball game");
            window.SetFramerateLimit(FPSLimit);

            Ball = InitShape(BallSize, new Vector2f(600f, 300f), Color.Red);

            LeftBat = InitPlatform(new Vector2f(BatWidth, BatHeight), new Vector2f(0f, BatPosY), Color.Yellow);
            RightBat = InitPlatform(new Vector2f(BatWidth, BatHeight), new Vector2f(BatTwoPosX, BatPosY), Color.Magenta);

            SpeedOfBats = new Vector2f(0f, 5f);
            Velocity = new Vector2f(500f, 400f);
        }

        internal void Run()
        {
            Init();

            while (IsGameRun())
            {
                LoseMechanic();

                GetInput();

                TryMove();

                LogicOfCirle();

                LogicOfPlatforms();

                CollisonLogic();


                Draw();
            }
        }

        private void GetInput()
        {
            Key_W_Is_Pressed = false;
            Key_S_Is_Pressed = false;
            Key_ArrowUp_Is_Pressed = false;
            Key_ArrowDown_Is_Pressed = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                IsEndGame = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                Key_W_Is_Pressed = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                Key_S_Is_Pressed = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                Key_ArrowUp_Is_Pressed = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                Key_ArrowDown_Is_Pressed = true;
        }

        private void TryMove()
        {
            if (LeftBat.Position.Y == 0)
                Key_W_Is_Pressed = false;

            if (LeftBat.Position.Y == Height - BatHeight)
                Key_S_Is_Pressed = false;

            if (RightBat.Position.Y == 0)
                Key_ArrowUp_Is_Pressed = false;

            if (RightBat.Position.Y == Height - BatHeight)
                Key_ArrowDown_Is_Pressed = false;
        }

        private void LogicOfCirle()
        {
            Ball.Position += Velocity * timeStep;

            if (Ball.Position.Y + 2 * BallSize > Height && Velocity.Y > 0)
                Velocity.Y *= -1;

            if (Ball.Position.Y < 0 && Velocity.Y < 0)
                Velocity.Y *= -1;

            if (Ball.Position.X + 2 * BallSize > Width && Velocity.X > 0)
                Velocity.X *= -1;

            if (Ball.Position.X < 0 && Velocity.X < 0)
                Velocity.X *= -1;
        }

        private void LogicOfPlatforms()
        {
            if (Key_W_Is_Pressed == true)
                LeftBat.Position -= SpeedOfBats;

            if (Key_S_Is_Pressed == true)

                LeftBat.Position += SpeedOfBats;

            if (Key_ArrowUp_Is_Pressed == true)
                RightBat.Position -= SpeedOfBats;

            if (Key_ArrowDown_Is_Pressed == true)
                RightBat.Position += SpeedOfBats;
        }

        private int CollisonLogic()
        {
            if (LeftBat.Position.X + BatWidth > Ball.Position.X && LeftBat.Position.Y + BatHeight == Ball.Position.Y)
                Velocity *= -1;

            if (RightBat.Position.X < Ball.Position.X + 2 * BallSize && RightBat.Position.Y == Ball.Position.Y)
                Velocity *= -1;

            return 1;   //это недоделано, в будущем для текста 
        }
        private void Draw()
        {
            window.Clear();

            Ball.FillColor = Color.Red;
            Ball.Draw(window, RenderStates.Default);

            LeftBat.Draw(window, RenderStates.Default);
            RightBat.Draw(window, RenderStates.Default);

            window.Display();
        }

        private bool IsGameRun()
        {
            return window.IsOpen && !IsEndGame;
        }

        private CircleShape InitShape(float size, Vector2f position, Color color)
        {
            CircleShape Ball = new CircleShape(size);
            Ball.FillColor = color;
            Ball.Position = position;
            return Ball;
        }

        private RectangleShape InitPlatform(Vector2f size, Vector2f position, Color color)
        {
            RectangleShape Platfom = new RectangleShape(size);
            Platfom.FillColor = color;
            Platfom.Position = position;
            return Platfom;
        }

    }
}
