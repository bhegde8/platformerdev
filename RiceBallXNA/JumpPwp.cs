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


namespace RiceBallXNA
{
    public class JumpPwp
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle boundingRect;
        
        

        public bool grounded = false;
        bool solid;


        public JumpPwp() { }

        public JumpPwp(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;


            boundingRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
