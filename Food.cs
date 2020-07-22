using Microsoft.Xna.Framework;
using System;

namespace MonoSnake
{
    public class Food
    {
        public Vector2 Position { get; private set; }
        private Random random;
        private Point _gridSize;
        private Snake _snake;
        public bool IsMouldy { get; set; }
        public double Age { get; set; } = 0;
        private double MaxAge { get; set; } = 5;

        public Food(Point gridSize, Snake snake)
        {
            random = new Random();
            _snake = snake;
            _gridSize = gridSize;
            NewFood();
        }

        public void Update(double GameTime)
        {
            Age += GameTime;
            if (Age > MaxAge)
                IsMouldy = true;
        }

        public void NewFood()
        {
            var overlapsWithSnake = false;
            do
            {
                var x = random.Next(1, _gridSize.X);
                var y = random.Next(1, _gridSize.Y);
                overlapsWithSnake = false;
                foreach (var position in _snake.Position)
                {
                    if (position.X == x && position.Y == y)
                        overlapsWithSnake = true;
                }
                if (!overlapsWithSnake)
                {
                    Position = new Vector2(x, y);
                    IsMouldy = false;
                    Age = 0;
                }
            } while (overlapsWithSnake);

        }
    }
}
