using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal abstract class View
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick);

        public abstract void Update(GameTime gameTime);
    }
}
