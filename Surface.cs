using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoSnake
{
    public class Surface
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        int _blockSize;
        Texture2D _snakeBlock;
        Texture2D _foodBlock;
        Snake _snake;
        Food _food;
        Point _gridSize;
        int _score;
        SpriteFont _font;

        public Surface(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, int blockSize, Snake snake, Food food, Point gridSize,SpriteFont font)
        {
            _blockSize = blockSize;
            _graphics = graphics;
            _gridSize = gridSize;
            _font = font;
            _food = food;
            _spriteBatch = spriteBatch;
            _snakeBlock = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _snakeBlock.SetData(new Color[] { Color.Green });
            _foodBlock = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _foodBlock.SetData(new Color[] { Color.Red });
            _snake = snake;
        }

        public void Draw()
        {
            _spriteBatch.DrawString(_font, 
                $"Score : {_score.ToString()} Debug : Food={_food.Position.X},{_food.Position.Y}"
                , new Vector2(10, 2), Color.White);

            foreach (var position in _snake.Position)
            {
                _spriteBatch.Draw(_snakeBlock, new Rectangle(
                    new Point(
                        (int)(position.X * _blockSize) - _blockSize,
                        (int)(position.Y * _blockSize) - _blockSize + 25),
                    new Point(_blockSize, _blockSize)), Color.White);
            }

            _spriteBatch.Draw(_foodBlock, new Rectangle(
                new Point(
                    (int)(_food.Position.X * _blockSize) - _blockSize,
                    (int)(_food.Position.Y * _blockSize) - _blockSize + 25),
                new Point(_blockSize, _blockSize)), Color.White);
        }

        public void Update()
        {
            if(_snake.Position[0] == _food.Position)
            {
                _food.NewFood();
                _score++;
                _snake.Grow();
            }
        }
    }
}
