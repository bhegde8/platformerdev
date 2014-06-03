using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Level1
    {

        public int level;
        public Platform p1;


       
        


        public Level1(ContentManager Content) {
            
            p1 = new Platform(Content.Load<Texture2D>(@"platform/platformGreen1"), new Vector2(342, 520));
        }

        public bool isIntersecting() //check if the player intersects any of the platforms
        {
            if (Game1.player.boundingRect.Intersects(p1.boundingRect))
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
           
        }

        public void moveRight()
        {
            float p1X = p1.Position.X;
            float p1Y = p1.Position.Y;
            p1.Position = new Vector2(p1X - 5, p1Y); 
        }

        public void moveLeft()
        {
            float p1X = p1.Position.X;
            float p1Y = p1.Position.Y;
            p1.Position = new Vector2(p1X + 5, p1Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            p1.boundingRect = new Rectangle((int)p1.Position.X, (int)p1.Position.Y, p1.Texture.Width, p1.Texture.Height); //update bounding rectangle position
            p1.Draw(spriteBatch);
        }
    }
}
