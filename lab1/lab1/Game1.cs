using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
//second commit
namespace lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 fontPosition;
        SpriteFont font;
        string fontstring = "Hello world!";
        SoundEffect bgm;
        SoundEffect movesound;
        float speed=1f;
        
        // constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fontPosition = new Vector2(300,200);
            font = Content.Load<SpriteFont>(@"Font/Arial");
            bgm = Content.Load<SoundEffect>(@"Sound/track");
            movesound = Content.Load<SoundEffect>(@"Sound/spray");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundEffectInstance instance = bgm.CreateInstance();
            instance.IsLooped = true;
            instance.Play();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(Keyboard.GetState().IsKeyDown(Keys.Up)||Keyboard.GetState().IsKeyDown(Keys.Down)||Keyboard.GetState().IsKeyDown(Keys.Right)||Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movesound.Play();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                fontPosition.Y -= speed*(float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                fontPosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                fontPosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                fontPosition.X += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, fontstring, fontPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
