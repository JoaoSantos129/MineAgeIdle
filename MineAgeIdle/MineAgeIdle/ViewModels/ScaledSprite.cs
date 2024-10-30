using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineAgeIdle
{
    internal class ScaledSprite : Sprite
    {
        public int Width { get; }
        public int Height { get; }
        public Rectangle Rect { get; protected set; }

        public ScaledSprite(Texture2D texture, Vector2 position, int width, int height) : base(texture, position)
        {
            this.Width = width;
            this.Height = height;
            Rect = new Rectangle((int)position.X, (int)position.Y, width, height);
        }
    }
}
