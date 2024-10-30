using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MineAgeIdle
{
    internal class Button : ColoredSprite
    {
        private readonly int _id;

        public int Id { get { return _id; } }

        private bool _isHovered = false;
        private bool _isOnView = false;

        public bool IsHovered
        {
            get { return _isHovered; }
            set
            {
                _isHovered = value;
                if (!IsOnView) // Only change the color if the button is not pressed
                {
                    color = value ? Color.DarkGray : Color.White;
                }
            }
        }

        public bool IsOnView
        {
            get { return _isOnView; }
            set
            {
                _isOnView = value;
                color = value ? Color.Gray : Color.White;
            }
        }

        public Button(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, int id)
            : base(texture, position, width, height, color, backgroundColor)
        {
            this._id = id;
        }
    }
}
