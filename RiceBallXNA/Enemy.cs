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
    public class Enemy //just another one of those stupid classes that is used for sprites! I really could've made all sprites into one class, but I don't want to confuse myself at this point.
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle boundingRect;

        public int jumpCount = 0;

        public bool grounded = true;
        bool solid;


        public Enemy() { } //i really hope you don't do this!

        public Enemy(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;


            boundingRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

        public void Jump()
        {
            if (grounded)
            {
                jumpCount = 0;
                float x10 = Position.X;
                float y10 = Position.Y;

                Position = new Vector2(x10, y10 - 3);
                boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                jumpCount++;
                grounded = false;
            }
            
            if (jumpCount == 0)
            {
                addGravity();
            }

            if (!grounded && jumpCount != 0)
            {
                float x11 = Position.X;
                float y11 = Position.Y;

                Position = new Vector2(x11, y11 - 3);
                boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
                jumpCount++;
            }

            if (jumpCount >= 90)
            {
                jumpCount = 0;

            }

            
            
        }

        public void cancelGravity()
        {
            grounded = true;
            float x10 = Position.X;
            float y10 = Position.Y;

            Position = new Vector2(x10, y10 - 2);
            boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public void addGravity()
        {
            float x10 = Position.X;
            float y10 = Position.Y;

            Position = new Vector2(x10, y10 + 2);
            boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
