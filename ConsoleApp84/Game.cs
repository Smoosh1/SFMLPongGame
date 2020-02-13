using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Program
{
    internal class Game
    {
        private const uint Height = 900;
        private const uint Width = 1800;
        private const int FPSLimit = 120;
        private uint BallSize = 50;

        private float timeStep = 1f / FPSLimit;

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

        public void Init()
        {
            window = new RenderWindow(new VideoMode(Width, Height), "Ball game");
            window.SetFramerateLimit(FPSLimit);

            Ball = InitShape(BallSize, new Vector2f(600f, 300f), Color.Red);

            LeftBat = InitPlatform(new Vector2f(50f, 300f), new Vector2f(0f, 300f), Color.Yellow);
            RightBat = InitPlatform(new Vector2f(50f, 300f), new Vector2f(1750f, 300f), Color.Magenta);

            SpeedOfBats = new Vector2f(0f, 5f);
            Velocity = new Vector2f(500f, 400f);
        }



        internal void Run()
        {
            Init();

            while (IsGameRun())
            {
                GetInput();

                TryMove();


                LogicOfCirle();

                LogicOfPlatforms();

                CollisonLogic();

                Draw();

            }
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

        private bool IsGameRun()
        {
            return window.IsOpen && !IsEndGame;
        }

        private void TryMove()
        {
            if (LeftBat.Position.Y == 0)
                Key_W_Is_Pressed = false;

            if (LeftBat.Position.Y == 600)
                Key_S_Is_Pressed = false;

            if (RightBat.Position.Y == 0)
                Key_ArrowUp_Is_Pressed = false;

            if (RightBat.Position.Y == 600)
                Key_ArrowDown_Is_Pressed = false;






        }

        private void CollisonLogic()
        {


            if (LeftBat.Position.X + 50f > Ball.Position.X)
                Velocity *= -1;

            if (RightBat.Position.X < Ball.Position.X + 2 * BallSize)
                Velocity *= -1;



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
ы
    }
}