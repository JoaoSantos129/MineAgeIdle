using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal class RotatingSprite : ColoredSprite
    {
        // Rotation angle in radians
        public float Rotation { get; set; } // The current angle of rotation in radians

        public Vector2 Origin { get; set; } // The point around which the sprite rotates

        public RotatingSprite(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, float rotation = 0f)
            : base(texture, position, width, height, color, backgroundColor)
        {
            this.Rotation = rotation;
            this.Origin = new Vector2(width / 2f, height / 2f); // Set the origin to the center by default
        }

        public override void Update()
        {
            base.Update();
        }

        // Since rotation changes how the sprite is drawn, override the Draw method
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,                     // Texture to draw
                position,                    // Position of the sprite on screen
                Rect,                        // Source rectangle (entire texture in this case)
                color,                       // Color mask (White means no tinting)
                Rotation,                    // Rotation angle in radians
                Origin,                      // Origin point for rotation (default is center)
                1f,                          // Scale (default 1f for no scaling)
                SpriteEffects.None,          // No sprite effects (like flipping)
                0f                           // Layer depth
            );
        }
    }
}
