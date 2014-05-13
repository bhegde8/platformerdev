using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Threading;

namespace RiceBallXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D background;
        private Texture2D orb;
        private Texture2D startButton;
        private Texture2D creditsButton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;
        private Texture2D loadScreen;

        

        private Vector2 orbPos;
        private Vector2 startButtonPos;
        private Vector2 creditsButtonPos;
        private Vector2 resumeButtonPos;
        private Vector2 backgroundPos;

        private const float OrbX = 50f;
        private const float OrbY = 50f;
        private float speed = 1.5f;

        private Thread daemonThread;
        private bool isLoading = false;
        MouseState mouseState;
        MouseState preMouseState;

        enum GameState
        {
            StartMenu,
            Loading,
            Playing,
            Paused
        }

        private GameState gs;

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

            IsMouseVisible = true;

            orbPos = new Vector2(50, 50);
            orb = Content.Load<Texture2D>(@"character/char1-1");

            backgroundPos = new Vector2(0, 0);

            startButtonPos = new Vector2(275, 240);

            creditsButtonPos = new Vector2(275, 440);

            

            gs = GameState.StartMenu;

           // LoadGame();

            mouseState = Mouse.GetState();
            preMouseState = mouseState;

            Song bgSound;
            bgSound = Content.Load<Song>("Waterflame - Glorious Morning 2");

            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(bgSound);

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

            // TODO: use this.Content to load your game content here

            background = Content.Load<Texture2D>(@"background/mainMenuNew");
            startButton = Content.Load<Texture2D>(@"background/startButton");
            creditsButton = Content.Load<Texture2D>(@"background/creditsButton");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            orbPos.X += speed;

            if (orbPos.X > (GraphicsDevice.Viewport.Width - OrbX) || orbPos.X < 0)
            {
                speed *= -1;
            }

            mouseState = Mouse.GetState();

            if (preMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                onMouseFire(mouseState.X, mouseState.Y);
            }

            preMouseState = mouseState;

            if (gs == GameState.Playing && isLoading)
            {
                LoadGame();
                isLoading = false;
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

            spriteBatch.Begin();

            if (gs == GameState.Playing)
            {
                
                spriteBatch.Draw(orb, orbPos, Color.White);

            }
            if (gs == GameState.StartMenu)
            {
                spriteBatch.Draw(background, backgroundPos, Color.White);
                spriteBatch.Draw(startButton, startButtonPos, Color.White);
                spriteBatch.Draw(creditsButton, creditsButtonPos, Color.White);

                spriteBatch.Draw(orb, orbPos, Color.White);
            }
            spriteBatch.End();

           
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void LoadGame()
        {
            orb = Content.Load<Texture2D>(@"character/char1-1");

            orbPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - (OrbX / 2), (GraphicsDevice.Viewport.Height / 2) - (OrbY / 2));
        }

        void onMouseFire(int x, int y)
        {
            Rectangle mouseHit = new Rectangle(x, y, 10, 10);

            if (gs == GameState.StartMenu)
            {
                Rectangle startButtonHit = new Rectangle((int)startButtonPos.X, (int)startButtonPos.Y, 262, 163);
                Rectangle creditsButtonHit = new Rectangle((int)creditsButtonPos.X, (int)creditsButtonPos.Y, 262, 163);

                if (mouseHit.Intersects(startButtonHit)) //clicked start button
                {
                    //gs = GameState.Loading;
                    gs = GameState.Playing;
                    isLoading = true;
                }

                else if (mouseHit.Intersects(creditsButtonHit))
                {
                    //open credits
                    //make a gamestate for credits
                    //render a new menu for credits:
                        //Have a texture2D for the credits text image
                        //Have a texture2D to exit

                    //add a if(gs == GameState.Credits) in the onMouseFire function
                    //if mouseHit intersects the "back" button rect:
                        //gs = GameState.StartMenu to load the start menu

                    //simple?
                }
            }
        }
    }
}
