using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineAgeIdle
{
    internal class MountainButton : Button
    {
        public MountainButton(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, int id)
            : base(texture, position, width, height, color, backgroundColor, id)
        {
        }
    }
}
