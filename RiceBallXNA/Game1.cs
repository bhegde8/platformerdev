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

        private int uselessCounter;

        private bool allowLS;
        private bool allowRS;

        public float jumpEndurance = 2f; //jumping endurance to limit how often the player can jump

        public int coins = 0;

        private static bool squashing = false;
        private static int squashCount = 0;

        public Level1 l1;
        public Level2 l2;


        public int playerHealth = 5;

        private Texture2D health1;
        private Texture2D health2;
        private Texture2D health3;
        private Texture2D health4;
        private Texture2D health5;

       //private Texture2D playerGraphic;

      //  private Vector2 playerPos;

        public static Sprite player;

        private Texture2D buildingScreen;
        private Texture2D buildingBar;

        string line;
        int count = 2;

        bool createdLevel = false;
        public static string[] levelCurList; //list of all the lines in a file
   
        private int llstatCurrent; //when building a level, how many lines have been processed in the level file

        private static Sprite[] spriteArray;
        
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
        private Texture2D loseScreen;
        private Texture2D winScreen;

        private Texture2D lcAR; //level chooser arrow right
        private Texture2D lcAL; //level choose arrow left

       // string curLevelProcessing;

        //private FileInfo[] levelList;

      //  DirectoryInfo directory = new DirectoryInfo("%USERPROFILE%\\Documents\\RiceballLevels"); //fix this!

        private string sprite = " ";

        public Platform[] test;

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

        private KeyboardState OldKeyState;

        private Vector2 skyPos;

        private bool isDead = false; //player is dead
        private bool hasWon = false; //player won

        private SoundEffect dingSound; //ding!
        private SoundEffect hitSound; //punch

        private SoundEffect jumpSound; //this only applies to the jumping powerup, not normal jumping

        private SoundEffect speedUp; //speed powerup

        private SoundEffect getCoin; //getting a coin

        public float accel = 2f;
        public string playerMove = "none";
        public int playerMoveCounter = 0;

        char[] space;
        private Texture2D sky;

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

        public static List<Platform> gamePlatforms;

        public static List<Sprite> spriteList;

        int gravityTotalI = 0;
        int gravityCounterI = 0;

        

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

            levelCurList[1] = "platformRed1 342 520";

            startButtonPos = new Vector2(275, 240);

            skyPos = new Vector2(0, 0);

            creditsButtonPos = new Vector2(275, 440);

            player = new Sprite();
            
            player.Position = new Vector2(342, 432);

         

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

            OldKeyState = Keyboard.GetState();
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

            sky = Content.Load<Texture2D>(@"background/sky");

            aPix = Content.Load<SpriteFont>("arcadepix");

            buildingBar = Content.Load<Texture2D>(@"background/buildingBar");
            buildingScreen = Content.Load<Texture2D>(@"background/buildingScreen");

            loseScreen = Content.Load<Texture2D>(@"background/loseScreen");

            winScreen = Content.Load<Texture2D>(@"background/winScreen");

            player.Texture = Content.Load<Texture2D>(@"character/char1-2");

            gameSongs = new List<Song>();

            

            gameSongs.Add(Content.Load<Song>(@"Waterflame - Glorious Morning 2"));
            gameSongs.Add(Content.Load<Song>(@"Waterflame - Jumper 2013 (HD)"));
            gameSongs.Add(Content.Load<Song>(@"Waterflame - To The Skies"));
            gameSongs.Add(Content.Load<Song>(@"Dimrain47 - The Prototype"));
            gameSongs.Add(Content.Load<Song>(@"Techno"));

            gamePlatforms = new List<Platform>();

            uselessCounter = 0; //i don't know actually, i'm sorry about this one

            dingSound = Content.Load<SoundEffect>("ding.wav");
            hitSound = Content.Load<SoundEffect>("hit");
            jumpSound = Content.Load<SoundEffect>("jump");
            getCoin = Content.Load<SoundEffect>("getCoin");

            health1 = Content.Load<Texture2D>(@"powerup/heart");
            health2 = Content.Load<Texture2D>(@"powerup/heart");
            health3 = Content.Load<Texture2D>(@"powerup/heart");
            health4 = Content.Load<Texture2D>(@"powerup/heart");
            health5 = Content.Load<Texture2D>(@"powerup/heart");


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

        /*public static string getWord(string inputString, int word)
        {
            string[] words = inputString.Split(new char[] { ' ' });
            if (words.Length >= word)
                return words[word];  // Using -1 so 2 returns second word - arrays are indexed with 0 being the first, normally, 
            // but since you asked for 2 to be the second word, this will do it
            else
                return "";
        }*/
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
                    if(uselessCounter > 0)
                    {
                        if(orb.Equals(Content.Load<Texture2D>(@"character/char1-1")))
                        {
                            orb = Content.Load<Texture2D>(@"character/char1-2");
                        }
                        else
                        {
                            orb = Content.Load<Texture2D>(@"character/char1-1");

                        
                        }
                    }
                    uselessCounter++;
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
                if (curSong != gameSongs.ElementAt(Convert.ToInt32(levelCurList[0])) && !isDead) //hacky method: use the indication of whether or not the correct song is playing to render the sprites
                {
                    StopSong();
                    int song = Convert.ToInt32(levelCurList[0]);
                    PlaySong(gameSongs[song]);




                    l1 = new Level1(Content);
                    l2 = new Level2(Content);

                     // increase jumping endurance
                        
                        //count++;
    //                    }
                }
                checkKeyPress();

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
                if (jumpEndurance < 30f)
                {
                    jumpEndurance = jumpEndurance + 0.15f;
                }
                else
                {
                    jumpEndurance = 2f;
                }
                spriteBatch.Draw(sky, skyPos, Color.White);

                player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);

                

                if (levelCurPath == "Content/Levels/Riceball - Level 1.txt" && !l1.Equals(null))
                {
                    l1.Draw(spriteBatch);

                }

                if (levelCurPath == "Content/Levels/Riceball - Level 2.txt" && !l2.Equals(null))
                {
                    l2.Draw(spriteBatch);

                }

                if (playerMove == "up")
                {
                    Console.WriteLine("DEBUG ENTERED PLAYERMOVE CHECK");
                    float x = player.Position.X;
                    float y = player.Position.Y;

                    player.Position = new Vector2(x, y-10);
                    player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);
                    playerMove = "none"; 
                }

                if (playerMove == "right") //if the player was trying to move right
                {
                    if (levelCurPath == "Content/Levels/Riceball - Level 1.txt")
                    {
                        l1.moveRight(); //move the platforms in the level to the right
                    }
                    if (levelCurPath == "Content/Levels/Riceball - Level 2.txt")
                    {
                        l2.moveRight(); //move the platforms in the level to the right
                    }
                    playerMove = "none";
                }

                if (playerMove == "left") //if the player was trying to move left
                {
                    if (levelCurPath == "Content/Levels/Riceball - Level 1.txt")
                    {
                        l1.moveLeft(); //move the platforms in the level to the left
                    }
                    if (levelCurPath == "Content/Levels/Riceball - Level 2.txt")
                    {
                        l2.moveLeft(); //move the platforms in the level to the left
                    }
                    playerMove = "none";
                }

                //GRAVITY-APPLY GRAVITY TO THE PLAYER!-------------------------------------------------------------------------------------------
                float x2 = player.Position.X;
                float y2 = player.Position.Y;
                
                player.Position = new Vector2(x2, y2+2);
                player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);
                //--------------------------------------------------------------------------------------------------------------------------------

                if (levelCurPath == "Content/Levels/Riceball - Level 1.txt") //level specific code
                
{
                    if (l1.isIntersecting()) //check if any of the objects in this level are colliding (box collision) with the player
                    {


                        float y3 = player.Position.Y;
                        float x3 = player.Position.X;

                        player.Position = new Vector2(x2, y3 - 3); //cancel the force of gravity if the player is on top of a platform
                        player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);


                        if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-1")))
                        {




                            player.Texture = Content.Load<Texture2D>(@"character/char1-1");
                        }
                        else if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-2")))
                        {





                            player.Texture = Content.Load<Texture2D>(@"character/char1-2");
                        }

                    }

                    if (l1.isKilling()) //if the player is killing an enemy in this level
                    {
                        coins = coins + 15;
                        hitSound.Play();
                    }

                    if (l1.isDying()) //if an enemy in this level is attacking the player
                    {

                        l1.moveLeft();
                        l1.moveLeft();
                        l1.moveLeft();
                        l1.moveLeft();
                        l1.moveLeft();
                        l1.moveLeft();
                        l1.moveLeft();

                        dingSound.Play();
                    }

                    if (l1.getsJumpPowerup())
                    {
                        jumpSound.Play();
                        float y3 = 22;
                        float x3 = player.Position.X;

                        player.Position = new Vector2(x3, y3); //teleport player to top
                        player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);
                    }

                    if (l1.getsSpeedPowerup())
                    {
                        jumpSound.Play();
                        for (int i = 0; i < 300; i++)
                        {

                            l1.moveRight();

                        }
                    }

                    if (l1.getsCoin())
                    {
                        getCoin.Play();
                        coins = coins + 10;
                    }

                    if (l1.getsToLeave())
                    {
                        hasWon = true;
                    }
                }       

                if (levelCurPath == "Content/Levels/Riceball - Level 2.txt") //level specific code
                {
                    if (l2.isIntersecting()) //check if any of the objects in this level are colliding (box collision) with the player
                    {


                        float y3 = player.Position.Y;
                        float x3 = player.Position.X;

                        player.Position = new Vector2(x2, y3 - 3); //cancel the force of gravity if the player is on top of a platform
                        player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);


                        if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-1")))
                        {




                            player.Texture = Content.Load<Texture2D>(@"character/char1-1");
                        }
                        else if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-2")))
                        {





                            player.Texture = Content.Load<Texture2D>(@"character/char1-2");
                        }

                    }

                    if (l2.isKilling()) //if the player is killing an enemy in this level
                    {
                        coins = coins + 15;
                        hitSound.Play();
                    }

                    if (l2.isDying()) //if an enemy in this level is attacking the player
                    {

                        l2.moveLeft();
                        l2.moveLeft();
                        l2.moveLeft();
                        l2.moveLeft();
                        l2.moveLeft();
                        l2.moveLeft();
                        l2.moveLeft();

                        dingSound.Play();
                    }

                    if (l2.getsJumpPowerup())
                    {
                        jumpSound.Play();
                        float y3 = 22;
                        float x3 = player.Position.X;

                        player.Position = new Vector2(x3, y3); //teleport player to top
                        player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);
                    }

                    if (l2.getsSpeedPowerup())
                    {
                        jumpSound.Play();
                        for (int i = 0; i < 300; i++)
                        {

                            l2.moveRight();

                        }
                    }

                    if (l2.getsCoin())
                    {
                        getCoin.Play();
                        coins = coins + 10;
                    }

                    if (l2.getsToLeave())
                    {
                        hasWon = true;
                    }
                }

                if (squashing)
                {

                    float y7 = player.Position.Y;
                    float x7 = player.Position.X;

                    player.Position = new Vector2(x7, y7 + 3);
                    player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);

                    if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char3-2"))) //player is squashing and is facing left
                    {
                        player.Texture = Content.Load<Texture2D>(@"character/char2-1");
                        squashing = false;
                    }
                    if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char3-1"))) //player is squashing and is facing right
                    {
                        player.Texture = Content.Load<Texture2D>(@"character/char2-2");
                        squashing = false;
                    }

                }
                

                //-------------------------------------------------------------------------------------------------
                
                Vector2 FontOrigin = aPix.MeasureString(jumpEndurance.ToString()) / 2; //jump endurance counter

                spriteBatch.DrawString(aPix, jumpEndurance.ToString(), new Vector2(780,30), Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                //-------------------------------------------------------------------------------------------------
                
                Vector2 aFontOrigin = aPix.MeasureString("$" + coins.ToString()) / 2; //coins counter

                if (coins < 50)
                {
                    spriteBatch.DrawString(aPix, "$" + coins.ToString(), new Vector2(400, 30), Color.Red, 0, aFontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                }
                else if (coins < 100 && coins >= 50)
                {
                    spriteBatch.DrawString(aPix, "$" + coins.ToString(), new Vector2(400, 30), Color.Yellow, 0, aFontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                }
                else if (coins >= 100)
                {
                    spriteBatch.DrawString(aPix, "$" + coins.ToString(), new Vector2(400, 30), Color.GreenYellow, 0, aFontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                }
                //-------------------------------------------------------------------------------------------------

                if (player.Position.Y > GraphicsDevice.Viewport.Height && !isDead) //check if player has fallen through the bottom of the level and is not already marked as dead
                {
                    
                    registerDeath();
                }

                if (isDead)
                {
                    if (jumpEndurance >= 29)
                    {
                        jumpEndurance = 0;
                        isDead = false;
                        gs = GameState.StartMenu;
                        player.Position = new Vector2(342, 432);
                        playerHealth = 5;
                        PlaySong(bgSound);
                        coins = 0;
                        squashing = false;
                        squashCount = 0;
                    }
                    spriteBatch.Draw(loseScreen, new Vector2(15, 15), Color.White);

                }

                if (hasWon)
                {
                    if (jumpEndurance >= 29)
                    {
                        jumpEndurance = 0;
                        isDead = false;
                        hasWon = false;
                        gs = GameState.StartMenu;
                        player.Position = new Vector2(342, 432);
                        playerHealth = 5;
                        PlaySong(bgSound);
                        coins = 0;
                        squashing = false;
                        squashCount = 0;
                    }
                    spriteBatch.Draw(winScreen, new Vector2(15, 15), Color.White);

                }

                player.Draw(spriteBatch);

                if(playerHealth == 1)
                {
                        
                        spriteBatch.Draw(health1, new Vector2(15, 15), Color.White);
                }
                else if(playerHealth == 2)
                {


                        spriteBatch.Draw(health1, new Vector2(15, 15), Color.White);
                        spriteBatch.Draw(health2, new Vector2(52, 15), Color.White);
                }
                else if(playerHealth == 3)
                {

               
                        spriteBatch.Draw(health1, new Vector2(15, 15), Color.White);
                        spriteBatch.Draw(health2, new Vector2(52, 15), Color.White);
                        spriteBatch.Draw(health3, new Vector2(89, 15), Color.White);
                }
                else if(playerHealth == 4)
                {
                        spriteBatch.Draw(health1, new Vector2(15, 15), Color.White);
                        spriteBatch.Draw(health2, new Vector2(52, 15), Color.White);
                        spriteBatch.Draw(health3, new Vector2(89, 15), Color.White);
                        spriteBatch.Draw(health4, new Vector2(126, 15), Color.White);
                }
                else if(playerHealth == 5)
                {
                    
                        spriteBatch.Draw(health1, new Vector2(15, 15), Color.White);
                        spriteBatch.Draw(health2, new Vector2(52, 15), Color.White);
                        spriteBatch.Draw(health3, new Vector2(89, 15), Color.White);
                        spriteBatch.Draw(health4, new Vector2(126, 15), Color.White);
                        spriteBatch.Draw(health5, new Vector2(163, 15), Color.White);
                        
                }
                 
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
                    count = 0;
                    levelCurList[0] = "";
                    levelCurList[1] = "";
                    levelCurList[2] = "";
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

        public static bool isSquashing()
        {
            return squashing;
        }

        public void registerDeath()
        {
            if (playerHealth == 1)
            {
                StopSong();
                dingSound.Play();

                playerHealth = 0;
                isDead = true;
                jumpEndurance = 0;
                coins = 0;
                squashing = false;
                squashCount = 0;
            }
            else
            {
                dingSound.Play();
                float y4 = player.Position.Y;
                float x4 = player.Position.X;

                player.Position = new Vector2(x4, y4 - 100); //recover them in a quick and dirty way that sucks (move them left and up)
                player.boundingRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);

                playerHealth--;

                squashing = false;
                squashCount = 0;

                
            }
        }
        void checkKeyPress()
        {
            //Console.WriteLine("DEBUG Entered checkKeyPress");
            KeyboardState NewKeyState = Keyboard.GetState();

            
            // Is the SPACE key down?
            
            if (NewKeyState.IsKeyDown(Keys.Space) && jumpEndurance >= 7.5f) //space = jump
            {
                jumpEndurance--;
              

                playerMove = "up";
                Console.WriteLine("Passed isKeyDown");
                    if(player.Texture.Equals(Content.Load<Texture2D>(@"character/char1-1")))
                    {

                        
                            
                        
                        player.Texture = Content.Load<Texture2D>(@"character/char2-1");
                    }
                    else if(player.Texture.Equals(Content.Load<Texture2D>(@"character/char1-2")))
                    {


                        
                            
                        
                        player.Texture = Content.Load<Texture2D>(@"character/char2-2");
                    }
                    return;
            }

            

            if (NewKeyState.IsKeyDown(Keys.D)) //D = move right       
            {
                playerMove = "right";
                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char1-1"))) //player is not jumping and is facing left
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char1-2");
                }
                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-1"))) //player is jumping and is facing left
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char2-2");
                }
                return;
            }

            if (NewKeyState.IsKeyDown(Keys.S)) //S = squash (force down)
            {
                

                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-1"))) //player is jumping and is facing left
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char3-2");
                    squashing = true;
                }
                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-2"))) //player is jumping and is facing right
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char3-1");
                    squashing = true;
                }
                return;
            }

            if (NewKeyState.IsKeyDown(Keys.A)) //A = move left      
            {
                playerMove = "left";
                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char1-2"))) //player is not jumping and is facing right
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char1-1");
                }
                if (player.Texture.Equals(Content.Load<Texture2D>(@"character/char2-2"))) //player is jumping and is facing right
                {
                    player.Texture = Content.Load<Texture2D>(@"character/char2-1");
                }
                return;
            }

           

            // Update saved state.
            OldKeyState = NewKeyState;
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
