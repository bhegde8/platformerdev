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
using System.IO;

namespace RiceBallXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int levelChooserCur; //current level selected in level chooser

        private bool allowLS;
        private bool allowRS;

        private Texture2D buildingScreen;
        private Texture2D buildingBar;

        string line;
        int count = 0;

        private static string[] levelCurList; //list of all the lines in a file
   
        private int llstatCurrent; //when building a level, how many lines have been processed in the level file

        private Texture2D background;
        private Texture2D orb;
        private Texture2D sOrb;
        private Texture2D startButton;
        private Texture2D creditsButton;
      //  private Texture2D pauseButton;
      //  private Texture2D resumeButton;
        //private Texture2D loadScreen;
        private Texture2D backButton;
        private Texture2D creditsBackground;
        private Texture2D loadMenuBackground;
        private Texture2D playButton;

        private Texture2D lcAR; //level chooser arrow right
        private Texture2D lcAL; //level choose arrow left

       // string curLevelProcessing;

        //private FileInfo[] levelList;

      //  DirectoryInfo directory = new DirectoryInfo("%USERPROFILE%\\Documents\\RiceballLevels"); //fix this!

        private Vector2 orbPos;
        private Vector2 sOrbPos;
        private Vector2 startButtonPos;
        private Vector2 creditsButtonPos;
      //  private Vector2 resumeButtonPos;
        private Vector2 backgroundPos;
        private Vector2 backButtonPos;
        private Vector2 creditsBackgroundPos;
        private Vector2 loadMenuBackgroundPos;
        private Vector2 playButtonPos;

        private bool beginCalled;

        private Vector2 buildingScreenPos;
        private Vector2 buildingBarPos;

        private Vector2 lcARPos;
        private Vector2 lcALPos;

        private string[] jiles; //text list of all level file names

        private Vector2 backButtonCredits = new Vector2(10, 10);
        private Vector2 backButtonChooser = new Vector2(45, 55);

        private const float OrbX = 50f;
        private const float OrbY = 50f;
        private float speed = 1.5f;

        private SpriteFont aPix;
        private Vector2 levelChooseTextPos;

        private string levelChooseText;

        private Thread daemonThread;
        private bool isLoading = false;
        MouseState mouseState;
        MouseState preMouseState;

        private Song curSong;

        public Song bgSound; //mainmenu

        public static List<Song> gameSongs;

        public string levelCurPath;

      //  public static SoundEffect click;


        enum GameState
        {
            StartMenu, //main menu
            Loading,   //credits
            Playing,   //inGame
            Paused,     //levelChooser
            Building    //loading screen
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


            levelCurList = new string[1999];

            

            orbPos = new Vector2(50, 50);
            orb = Content.Load<Texture2D>(@"character/char1-1");

            sOrbPos = new Vector2(50, 50);
            sOrb = Content.Load<Texture2D>(@"character/enemy1-2");

            backgroundPos = new Vector2(0, 0);

            creditsBackgroundPos = new Vector2(0, 0);

            startButtonPos = new Vector2(275, 240);

            creditsButtonPos = new Vector2(275, 440);

         

              //  backButtonPos = new Vector2(0, 0);

                loadMenuBackgroundPos = new Vector2(0, 0);

                playButtonPos = new Vector2(585, 55);

                lcALPos = new Vector2(45, 400);
                lcARPos = new Vector2(585, 400);

                levelChooseTextPos = new Vector2(400, 300);

                buildingScreenPos = new Vector2(0, 0);
                buildingBarPos = new Vector2(29, 483);




                jiles = File.ReadAllLines("Content/LevelList.txt");
                

                levelChooseText = jiles.GetValue(0).ToString();
                

                levelChooserCur = 0;

                levelCurPath = "Content/Levels/" + levelChooseText + ".txt";

            beginCalled = new bool();
            

            gs = GameState.StartMenu;

           // LoadGame();

            mouseState = Mouse.GetState();
            preMouseState = mouseState;

            
            bgSound = Content.Load<Song>(@"Techno");

            MediaPlayer.IsRepeating = true;

            PlaySong(bgSound);

            base.Initialize();
        }

        void StopSong()
        {
            curSong = bgSound;
            MediaPlayer.Stop();
        }

        void PlaySong(Song s)
        {
            curSong = s;
            MediaPlayer.Play(s);
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

            backButton = Content.Load<Texture2D>(@"background/backButton");
            creditsBackground = Content.Load<Texture2D>(@"background/creditsScreen");

            loadMenuBackground = Content.Load<Texture2D>(@"background/loadMenu");

            playButton = Content.Load<Texture2D>(@"background/playButton");

            lcAR = Content.Load<Texture2D>(@"background/levelChooseArrowRight");
            lcAL = Content.Load<Texture2D>(@"background/levelChooseArrowLeft");

            aPix = Content.Load<SpriteFont>("arcadepix");

            buildingBar = Content.Load<Texture2D>(@"background/buildingBar");
            buildingScreen = Content.Load<Texture2D>(@"background/buildingScreen");

            gameSongs = new List<Song>();

            gameSongs.Add(Content.Load<Song>(@"Waterflame - Glorious Morning 2"));
            gameSongs.Add(Content.Load<Song>(@"Waterflame - Jumper 2013 (HD)"));
            gameSongs.Add(Content.Load<Song>(@"Waterflame - To The Skies"));
            gameSongs.Add(Content.Load<Song>(@"Dimrain47 - The Prototype"));
            gameSongs.Add(Content.Load<Song>(@"Techno"));
        
                                    
            

            //creditsSong = Content.Load<Song>(@"Techno");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 
        
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

            if (gs == GameState.StartMenu)
            {

                orbPos.X += speed;

                if (orbPos.X > (GraphicsDevice.Viewport.Width - OrbX) || orbPos.X < 0)
                {
                    speed *= -1;
                }

            }

            if (gs == GameState.Building)
            {

                
            }

            if (gs == GameState.Loading)
            {
                sOrbPos.Y += speed;

                if (sOrbPos.Y > (GraphicsDevice.Viewport.Height - OrbY) || sOrbPos.Y < 0)
                {
                    speed *= -1;
                }
            }

            mouseState = Mouse.GetState();

            if (preMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                onMouseFire(mouseState.X, mouseState.Y);
            }

            preMouseState = mouseState;

            if (gs == GameState.Playing )
            {
                if (curSong != gameSongs.ElementAt(Convert.ToInt32(levelCurList[0])))
                {
                    StopSong();
                    int song = Convert.ToInt32(levelCurList[0]);
                    PlaySong(gameSongs[song]);
                }
            }

            if (gs == GameState.StartMenu && curSong != bgSound)
            {
                StopSong();
                PlaySong(bgSound);
            }

          //  if (gs == GameState.Loading && curSong != creditsSong)
           // {
          //      StopSong();
          //      PlaySong(creditsSong);
           // }

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
            beginCalled = true;

            if (gs == GameState.Playing)
            {
                
                spriteBatch.Draw(orb, orbPos, Color.White);

            }
            if (gs == GameState.Loading)
            {
                spriteBatch.Draw(creditsBackground, creditsBackgroundPos, Color.White);
                spriteBatch.Draw(backButton, backButtonPos, Color.White);
                spriteBatch.Draw(sOrb, sOrbPos, Color.White);
            }
            if (gs == GameState.StartMenu)
            {
                spriteBatch.Draw(background, backgroundPos, Color.White);
                spriteBatch.Draw(startButton, startButtonPos, Color.White);
                spriteBatch.Draw(creditsButton, creditsButtonPos, Color.White);

                spriteBatch.Draw(orb, orbPos, Color.White);
            }

            if (gs == GameState.Building)
            {
                spriteBatch.Draw(buildingScreen, buildingScreenPos, Color.White);
                

                using (StreamReader r = new StreamReader(levelCurPath))
                {

                    while ((line = r.ReadLine()) != null)
                    {

                        levelCurList[count] = line;
                        count++;

                        if (beginCalled)
                        {
                            Vector2 FontOrigin = aPix.MeasureString(levelChooseText) / 2;

                            spriteBatch.DrawString(aPix, "Loading Level...", levelChooseTextPos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                        }
                        buildingBarPos.X += speed;
                        spriteBatch.Draw(buildingBar, buildingBarPos, Color.White);

                        if (buildingBarPos.X > (GraphicsDevice.Viewport.Width - OrbX) || buildingBarPos.X < 0)
                        {
                            speed *= -1;
                        }

                    }

                }


                gs = GameState.Playing;
            }

            if (gs == GameState.Paused)
            {
                spriteBatch.Draw(loadMenuBackground, loadMenuBackgroundPos, Color.White);
                spriteBatch.Draw(backButton, backButtonPos, Color.White);
                spriteBatch.Draw(playButton, playButtonPos, Color.White);


                if (levelChooserCur != 0)
                {
                    allowLS = true;
                    spriteBatch.Draw(lcAL, lcALPos, Color.White);
                }
                else
                {
                    allowLS = false;
                }

                if (levelChooserCur != jiles.Length - 1)
                {
                    allowRS = true;
                    spriteBatch.Draw(lcAR, lcARPos, Color.White);
                }

                else
                {
                    allowRS = false;
                }

                Vector2 FontOrigin = aPix.MeasureString(levelChooseText) / 2;

                spriteBatch.DrawString(aPix, levelChooseText, levelChooseTextPos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
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
                    gs = GameState.Paused;
                    backButtonPos = backButtonChooser;
                    //isLoading = true;
                }

                else if (mouseHit.Intersects(creditsButtonHit))
                {
                    gs = GameState.Loading; //credits, ha i know it's dumb that i named it like this but originally there was a loading screen state
                    backButtonPos = backButtonCredits;
                }


            }

            if (gs == GameState.Paused)
            {
                

                Rectangle backButtonHit = new Rectangle((int)backButtonPos.X, (int)backButtonPos.Y, 163, 101);

                Rectangle leftHit = new Rectangle((int)lcALPos.X, (int)lcALPos.Y, 163, 101);
                Rectangle rightHit = new Rectangle((int)lcARPos.X, (int)lcARPos.Y, 163, 101);

                Rectangle playHit = new Rectangle((int)playButtonPos.X, (int)playButtonPos.Y, 163, 101);

                if (mouseHit.Intersects(backButtonHit)) //clicked back button
                {
                    gs = GameState.StartMenu;
                }

                else if (mouseHit.Intersects(leftHit) && allowLS)
                {
                   // levelChooseText = levelChooserCur.ToString();
                    if (levelChooserCur == 0)
                    {
                        levelChooserCur = jiles.Length;
                       // levelChooseText = levelChooserCur.ToString();
                       levelChooseText = jiles.GetValue(levelChooserCur).ToString();
                       levelCurPath = "Content/Levels/" + levelChooseText + ".txt";
                    }
                    else
                   {
                       levelChooserCur--;
                       levelChooseText = jiles.GetValue(levelChooserCur).ToString();
                       levelCurPath = "Content/Levels/" + levelChooseText + ".txt";
                    }
                    
                }

                else if (mouseHit.Intersects(rightHit) && allowRS)
                {
                    if (levelChooserCur == jiles.Length)
                    {
                        levelChooserCur = 0;
                        levelChooseText = jiles.GetValue(levelChooserCur).ToString();
                        levelCurPath = "Content/Levels/" + levelChooseText + ".txt";
                    }
                    else
                    {
                        levelChooserCur++;
                        levelChooseText = jiles.GetValue(levelChooserCur).ToString();
                        levelCurPath = "Content/Levels/" + levelChooseText + ".txt";
                    }
                    
                }

                else if (mouseHit.Intersects(playHit))
                {
                    gs = GameState.Building;
                }
            }

            if (gs == GameState.Loading)
            {
                

                Rectangle backButtonHit = new Rectangle((int)backButtonPos.X, (int)backButtonPos.Y, 163, 101);

                if (mouseHit.Intersects(backButtonHit)) //clicked back button
                {
                    gs = GameState.StartMenu;
                }
            }
        }
    }
}
