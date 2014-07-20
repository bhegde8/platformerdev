using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Level2
    {

        public static int level;

        public static Platform p1;
        public static Platform p2;
        public static Platform p3;
        public static Platform p4;

        public static Platform p5;
        public static Platform p6;
        public static Platform p7;
        public static Platform p8;

        public static CoinPwp c1;

        public static JumpPwp j1;

        public static SpeedPwp s1;

        public static Enemy e1;

        public static Door d1;




        public Level2(ContentManager Content)
        {

            p1 = new Platform(Content.Load<Texture2D>(@"platform/platformGreen1"), new Vector2(342, 520));

            p2 = new Platform(Content.Load<Texture2D>(@"platform/platformGreen2"), new Vector2(1000, 400));

            p3 = new Platform(Content.Load<Texture2D>(@"platform/platformGray1"), new Vector2(1670, 520));

            p4 = new Platform(Content.Load<Texture2D>(@"platform/platformBlack1"), new Vector2(2275, 80));

            p5 = new Platform(Content.Load<Texture2D>(@"platform/platformYellow2"), new Vector2(2775, 500));

            p6 = new Platform(Content.Load<Texture2D>(@"platform/platformYellow1"), new Vector2(3275, 500));

            p7 = new Platform(Content.Load<Texture2D>(@"platform/platformYellow2"), new Vector2(3775, 500));

            p8 = new Platform(Content.Load<Texture2D>(@"platform/platformBlack1"), new Vector2(4375, 520));

            j1 = new JumpPwp(Content.Load<Texture2D>(@"powerup/jump"), new Vector2(2020, 369));

            c1 = new CoinPwp(Content.Load<Texture2D>(@"powerup/coin"), new Vector2(1745, 445));

            e1 = new Enemy(Content.Load<Texture2D>(@"character/enemy1-1"), new Vector2(1115, 275));

            s1 = new SpeedPwp(Content.Load<Texture2D>(@"powerup/speed"), new Vector2(2775, 350));

            //d1 = new Door(Content.Load<Texture2D>(@"powerup/door"), new Vector2(4635, 260));

        }

        public bool isIntersecting() //check if the player intersects any of the platforms
        {
            if (Game1.player.boundingRect.Intersects(p1.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p2.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p3.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p4.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p5.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p6.boundingRect))
            {
                return true;
            }
            if (Game1.player.boundingRect.Intersects(p7.boundingRect))
            {
                return true;
            }


            return false;
        }

        public bool isKilling() //check if an enemy is being killed
        {
            if (e1 != null && Game1.player.boundingRect.Intersects(e1.boundingRect))
            {
                if (Game1.isSquashing()) //player is trying to squash 
                {
                    e1 = null; //cheap way of removing the enemy
                    return true; //grants points to the user for the kill
                }
            }
            return false;
        }

        public bool isDying() //check if the enemy is damaging the player
        {
            if (e1 != null && Game1.player.boundingRect.Intersects(e1.boundingRect))
            {
                if (!Game1.isSquashing())
                {
                    return true;
                }
            }
            return false;
        }

        public bool getsJumpPowerup() //check if the player is getting a jump powerup
        {

            if (j1 != null && Game1.player.boundingRect.Intersects(j1.boundingRect))
            {

                j1 = null;
                return true;
            }
            return false;
        }

        public bool getsSpeedPowerup() //check if the player is getting a speed powerup
        {
            if (s1 != null && Game1.player.boundingRect.Intersects(s1.boundingRect))
            {
                s1 = null;
                return true;
            }
            return false;
        }

        public bool getsCoin() //check if the player is getting a coin
        {
            if (c1 != null && Game1.player.boundingRect.Intersects(c1.boundingRect))
            {
                c1 = null;
                return true;
            }
            return false;
        }

        public bool getsToLeave() //check if the player gets to leave the stupid level
        {
           /* if (d1 != null && Game1.player.boundingRect.Intersects(d1.boundingRect))
            {
                return true;
            } 
            */
            return false;
        }

        public void Update() //i wonder if i'll ever use this method. I doubt it.
        {

        }

        public void moveRight()
        {
            float p1X = p1.Position.X;
            float p1Y = p1.Position.Y;
            p1.Position = new Vector2(p1X - 5, p1Y);

            float p2X = p2.Position.X;
            float p2Y = p2.Position.Y;
            p2.Position = new Vector2(p2X - 5, p2Y);

            float p3X = p3.Position.X;
            float p3Y = p3.Position.Y;
            p3.Position = new Vector2(p3X - 5, p3Y);

            float p4X = p4.Position.X;
            float p4Y = p4.Position.Y;
            p4.Position = new Vector2(p4X - 5, p4Y);


            float p5X = p5.Position.X;
            float p5Y = p5.Position.Y;
            p5.Position = new Vector2(p5X - 5, p5Y);

            float p6X = p6.Position.X;
            float p6Y = p6.Position.Y;
            p6.Position = new Vector2(p6X - 5, p6Y);

            float p7X = p7.Position.X;
            float p7Y = p7.Position.Y;
            p7.Position = new Vector2(p7X - 5, p7Y);

            if (e1 != null)
            {
                float e1X = e1.Position.X;
                float e1Y = e1.Position.Y;
                e1.Position = new Vector2(e1X - 5, e1Y);

            }

            if (j1 != null)
            {
                float j1X = j1.Position.X;
                float j1Y = j1.Position.Y;
                j1.Position = new Vector2(j1X - 5, j1Y);
            }

            if (c1 != null)
            {
                float c1X = c1.Position.X;
                float c1Y = c1.Position.Y;
                c1.Position = new Vector2(c1X - 5, c1Y);
            }

            if (s1 != null)
            {
                float s1X = s1.Position.X;
                float s1Y = s1.Position.Y;
                s1.Position = new Vector2(s1X - 5, s1Y);
            }

           /* if (d1 != null)
            {
                float d1X = d1.Position.X;
                float d1Y = d1.Position.Y;
                d1.Position = new Vector2(d1X - 5, d1Y);
            }*/
        }
        public void moveLeft()
        {
            float p1X = p1.Position.X;
            float p1Y = p1.Position.Y;
            p1.Position = new Vector2(p1X + 5, p1Y);

            float p2X = p2.Position.X;
            float p2Y = p2.Position.Y;
            p2.Position = new Vector2(p2X + 5, p2Y);

            float p3X = p3.Position.X;
            float p3Y = p3.Position.Y;
            p3.Position = new Vector2(p3X + 5, p3Y);

            float p4X = p4.Position.X;
            float p4Y = p4.Position.Y;
            p4.Position = new Vector2(p4X + 5, p4Y);

            float p5X = p5.Position.X;
            float p5Y = p5.Position.Y;
            p5.Position = new Vector2(p5X + 5, p5Y);

            float p6X = p6.Position.X;
            float p6Y = p6.Position.Y;
            p6.Position = new Vector2(p6X + 5, p6Y);

            float p7X = p7.Position.X;
            float p7Y = p7.Position.Y;
            p7.Position = new Vector2(p7X + 5, p7Y);

            if (e1 != null)
            {
                float e1X = e1.Position.X;
                float e1Y = e1.Position.Y;
                e1.Position = new Vector2(e1X + 5, e1Y);
            }

            if (j1 != null)
            {
                float j1X = j1.Position.X;
                float j1Y = j1.Position.Y;
                j1.Position = new Vector2(j1X + 5, j1Y);
            }

            if (c1 != null)
            {
                float c1X = c1.Position.X;
                float c1Y = c1.Position.Y;
                c1.Position = new Vector2(c1X + 5, c1Y);
            }

            if (s1 != null)
            {
                float s1X = s1.Position.X;
                float s1Y = s1.Position.Y;
                s1.Position = new Vector2(s1X + 5, s1Y);
            }

           /* if (d1 != null)
            {
                float d1X = d1.Position.X;
                float d1Y = d1.Position.Y;
                d1.Position = new Vector2(d1X + 5, d1Y);
            }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            p1.boundingRect = new Rectangle((int)p1.Position.X, (int)p1.Position.Y, p1.Texture.Width, p1.Texture.Height); //update bounding rectangle position
            p1.Draw(spriteBatch);

            p2.boundingRect = new Rectangle((int)p2.Position.X, (int)p2.Position.Y, p2.Texture.Width, p2.Texture.Height); //update bounding rectangle position
            p2.Draw(spriteBatch);

            p3.boundingRect = new Rectangle((int)p3.Position.X, (int)p3.Position.Y, p3.Texture.Width, p3.Texture.Height); //update bounding rectangle position
            p3.Draw(spriteBatch);

            p4.boundingRect = new Rectangle((int)p4.Position.X, (int)p4.Position.Y, p4.Texture.Width, p4.Texture.Height); //update bounding rectangle position
            p4.Draw(spriteBatch);


            p5.boundingRect = new Rectangle((int)p5.Position.X, (int)p5.Position.Y, p5.Texture.Width, p5.Texture.Height); //update bounding rectangle position
            p5.Draw(spriteBatch);

            p6.boundingRect = new Rectangle((int)p6.Position.X, (int)p6.Position.Y, p6.Texture.Width, p6.Texture.Height); //update bounding rectangle position
            p6.Draw(spriteBatch);

            p7.boundingRect = new Rectangle((int)p7.Position.X, (int)p7.Position.Y, p7.Texture.Width, p7.Texture.Height); //update bounding rectangle position
            p7.Draw(spriteBatch);

            if (e1 != null)
            {
                e1.addGravity();
                if (e1.boundingRect.Intersects(p1.boundingRect) || e1.boundingRect.Intersects(p2.boundingRect))
                {
                    e1.cancelGravity();
                }
                e1.Jump();
                e1.boundingRect = new Rectangle((int)e1.Position.X, (int)e1.Position.Y, e1.Texture.Width, e1.Texture.Height); //update bounding rectangle position
                e1.Draw(spriteBatch);

            }

            if (j1 != null)
            {
                j1.boundingRect = new Rectangle((int)j1.Position.X, (int)j1.Position.Y, j1.Texture.Width, j1.Texture.Height);
                j1.Draw(spriteBatch);
            }

            if (c1 != null)
            {
                c1.boundingRect = new Rectangle((int)c1.Position.X, (int)c1.Position.Y, c1.Texture.Width, c1.Texture.Height);
                c1.Draw(spriteBatch);
            }

            if (s1 != null)
            {
                s1.boundingRect = new Rectangle((int)s1.Position.X, (int)s1.Position.Y, s1.Texture.Width, s1.Texture.Height);
                s1.Draw(spriteBatch);
            }

         /*   if (d1 != null)
            {
                d1.boundingRect = new Rectangle((int)d1.Position.X, (int)d1.Position.Y, d1.Texture.Width, d1.Texture.Height);
                d1.Draw(spriteBatch);
            } */
        }
    }
}
