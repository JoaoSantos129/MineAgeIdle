using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal class ColoredSprite : ScaledSprite
    {
        public Color color { get; set; }
        public Color backgroundColor;

        public ColoredSprite(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor) : base(texture, position, width, height)
        {
            this.color = color;
            this.backgroundColor = backgroundColor;
        }
    }
}
