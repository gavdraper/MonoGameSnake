using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoSnake
{


    public enum GameState
    {
        Playing,
        GameOver
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Surface surface;
        private Snake snake;
        private Food food;
        private GameState state = GameState.Playing;

        public Point GridSize = new Point(20, 20);
        public int blockSize = 20;
        SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            _graphics.PreferredBackBufferWidth = (blockSize * GridSize.X) + 25;
            _graphics.PreferredBackBufferHeight = (blockSize * GridSize.Y) + 25;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("SnakeFont");
            newGame();
        }

        private void newGame()
        {
            state = GameState.Playing;
            snake = new Snake(Math.Abs(GridSize.X / 2), Math.Abs(GridSize.Y / 2));
            food = new Food(GridSize,snake);
            surface = new Surface(_graphics, _spriteBatch, blockSize, snake, food,GridSize,_font);
        }

        private bool CheckSnakeOutOfBounds()
        {
            return snake.Position[0].X <= 0 || snake.Position[0].Y <= 0
                    || snake.Position[0].X > GridSize.X
                    || snake.Position[0].Y > GridSize.Y;
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (state == GameState.Playing)
            {
                surface.Update();
                if (CheckSnakeOutOfBounds() || snake.SnakeIntersectsSnake)
                {
                    state = GameState.GameOver;
                }
                snake.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                //Game over
                var keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    newGame();
                    
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (state == GameState.Playing)
            {
                surface.Draw();
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.DrawString(_font, "Press Space For New Game", new Vector2(0, 0), Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
