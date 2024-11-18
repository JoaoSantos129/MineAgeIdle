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
        private bool isActive;
        private float cooldownSeconds;
        private float currentCooldown = 0f;

        public int Id { get { return _id; } }

        private bool _isHovered = false;
        private bool _isOnView = false;

        public Button(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, int id, bool isActive, float cooldownSeconds)
            : base(texture, position, width, height, color, backgroundColor)
        {
            this._id = id;
            this.isActive = true;
            this.cooldownSeconds = cooldownSeconds;
        }

        public void Update(GameTime gameTime)
        {
            if (!isActive)
            {
                currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentCooldown <= 0)
                {
                    isActive = true;
                    currentCooldown = 0; // Reset cooldown
                }
            }

            RefreshColor(); // Ensure color reflects the button's current state
        }

        public bool IsHovered
        {
            get { return _isHovered; }
            set
            {
                _isHovered = value;
                RefreshColor();
            }
        }

        public bool IsOnView
        {
            get { return _isOnView; }
            set
            {
                _isOnView = value;
                RefreshColor();
            }
        }

        public void ButtonOnCooldown()
        {
            if (isActive)
            {
                isActive = false;
                currentCooldown = cooldownSeconds;
            }
        }

        public bool IsActive()
        {
            return isActive;
        }

        public bool ProcessClick(Action onClick)
        {
            if (!isActive) return false; // Ignore clicks if inactive

            onClick?.Invoke(); // Execute the action associated with the click
            return true;
        }

        private void RefreshColor()
        {
            if (!isActive)
            {
                color = Color.DarkGray; // Cooldown state
            }
            else if (IsOnView)
            {
                color = Color.Gray; // On view
            }
            else if (IsHovered)
            {
                color = Color.DarkGray; // Hovered
            }
            else
            {
                color = Color.White; // Default state
            }
        }
    }
}