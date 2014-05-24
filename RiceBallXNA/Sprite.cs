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
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle boundingRect;
        public Rectangle gravityRect;

        public bool grounded = false;
        bool solid;
        

        public Sprite() { }

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;

            boundingRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            gravityRect = new Rectangle((int)boundingRect.X, (int)boundingRect.Y - 3, texture.Width, texture.Height + 3);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
