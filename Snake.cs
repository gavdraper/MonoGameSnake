using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;
using System.Linq;

namespace MonoSnake
{
    public class Snake
    {
        //Todo : Change the speed of the snake based on current score.

        public List<Vector2> Position { get; set; } = new List<Vector2>();
        public Vector2 Direction = new Vector2(0, -1);
        public double snakeMoveTime = 0.10;
        private bool growing = false;
        public bool SnakeIntersectsSnake { get; set; }
        private bool directionChanged;

        public Snake(int startingX, int startingY)
        {
            Position.Add(new Vector2(startingX, startingY));
        }

        private double timeSinceSnakeMove = 0;
        public void Update(double timeSinceLastUpdate)
        {
            timeSinceSnakeMove += timeSinceLastUpdate;
            if (timeSinceSnakeMove >= snakeMoveTime || directionChanged)
            {

                directionChanged = false;
                timeSinceSnakeMove = 0;
                var endOfSnakeBeforeMove = new Vector2(Position.Last().X, Position.Last().Y);
                //Move Existing Snake

                //Tail
                for (var i = Position.Count() - 1; i > 0; i--)
                {
                    Position[i] = Position[i - 1];
                }
                //Head
                Position[0] += Direction;


                //Check Snake Collision with Snake
                for (var i = 1; i < Position.Count; i++)
                {
                    if (Position[0].X == Position[i].X && Position[0].Y == Position[i].Y)
                    {
                        SnakeIntersectsSnake = true;
                    }
                }

                //GrowSnake
                if (growing)
                {
                    Position.Add(endOfSnakeBeforeMove);
                    growing = false;
                }
            }

            if (!directionChanged)
            {
                KeyboardState keyboardState = Keyboard.GetState();

                Vector2 newDirection = Direction;
                var keys = keyboardState.GetPressedKeys();
                if (keys.Any())
                {
                    var lastKey = keyboardState.GetPressedKeys().Last();

                    if (lastKey == Keys.Left && Direction.X != 1 && Direction.X != -1)
                        newDirection = new Vector2(-1, 0);
                    else if (lastKey == Keys.Right && Direction.X != -1 && Direction.X != 1)
                        newDirection = new Vector2(1, 0);
                    else if (lastKey == Keys.Up && Direction.Y != 1 && Direction.Y != -1)
                        newDirection = new Vector2(0, -1);
                    else if (lastKey == Keys.Down && Direction.Y != -1 && Direction.Y != 1)
                        newDirection = new Vector2(0, 1);

                    if (newDirection != Direction)
                    {
                        Direction = newDirection;
                        System.Diagnostics.Debug.WriteLine("Left");
                        directionChanged = true;
                    }
                }
            }
        }

        public void Grow()
        {
            growing = true;
        }

    }
}
