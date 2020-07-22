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

        public Food(Point gridSize, Snake snake)
        {
            random = new Random();
            _snake = snake;
            _gridSize = gridSize;
            NewFood();
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
                if(!overlapsWithSnake)
                    Position = new Vector2(x, y);
            } while (overlapsWithSnake);
            
        }
    }
}
