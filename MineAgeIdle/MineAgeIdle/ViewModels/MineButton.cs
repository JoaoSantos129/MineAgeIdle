﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineAgeIdle
{
    internal class MineButton : Button
    {
        public MineButton(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, int id, bool isActive, float cooldownSeconds)
            : base(texture, position, width, height, color, backgroundColor, id, isActive, cooldownSeconds)
        {
        }
    }
}
