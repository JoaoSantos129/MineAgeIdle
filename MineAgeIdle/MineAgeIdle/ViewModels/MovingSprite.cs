using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal class MovingSprite : ColoredSprite
    {
        protected float rotation; // Add a rotation property
        protected float rotationSpeed; // Optional: Speed of rotation
        private float initialRotation; // Track the initial rotation for resetting
        private float targetRotation; // Target rotation in radians
        private bool isReversing; // Determines if the rotation is reversing
        private float rotationAmount; // Amount of rotation in degrees
        public bool hasDoneRotation; // Determines when sprite has done a full rotation
        private bool reverseRotation; // If true the sprite will start with a right to left rotation
        private float xSpeed;
        private float initialXPosition;
        private float currentXPosition;
        private float finalXPosition;
        private bool reverseXMove;
        private float ySpeed;
        private float initialYPosition;
        private float currentYPosition;
        private bool reverseYMove;
        public bool HasReachedFinalPosition { get; set; } = false;

        public MovingSprite(Texture2D texture, Vector2 position, int width, int height, Color color, Color backgroundColor, float rotation, float rotationSpeed, float rotationAmount, bool reverseRotation, float xSpeed, float currentXPosition, float finalXPosition, bool reverseXMove, float ySpeed, float currentYPosition, bool reverseYMove)
            : base(texture, position, width, height, color, backgroundColor)
        {
            this.rotation = rotation;
            this.initialRotation = rotation; // Save the starting rotation
            this.rotationSpeed = rotationSpeed; // Initialize rotation speed
            this.rotationAmount = rotationAmount; // Set the desired rotation amount
            this.targetRotation = MathHelper.ToRadians(rotationAmount); // Convert rotation amount to radians
            this.isReversing = false; // Initially, it is not reversing
            this.hasDoneRotation = false; // Initially, has not completed a rotation
            this.reverseRotation = reverseRotation;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.initialXPosition = currentXPosition;
            this.initialYPosition = currentYPosition;
            this.currentXPosition = currentXPosition;
            this.currentYPosition = currentYPosition;
            this.finalXPosition = finalXPosition;
            this.reverseXMove = reverseXMove;
            this.reverseYMove = reverseYMove;
    }

        public override void Update()
        {
            base.Update();

            if (xSpeed > 0 || ySpeed > 0)
            {
                // Update X position
                if (reverseXMove)
                {
                    if (currentXPosition > finalXPosition)
                        currentXPosition -= xSpeed;
                    else
                        currentXPosition = finalXPosition; // Snap to final position
                }
                else
                {
                    if (currentXPosition < finalXPosition)
                        currentXPosition += xSpeed;
                    else
                        currentXPosition = finalXPosition; // Snap to final position
                }

                // Update Y position
                if (reverseYMove)
                {
                    currentYPosition -= ySpeed;
                }
                else
                {
                    currentYPosition += ySpeed;
                }
            }

            // Update the sprite's position
            position = new Vector2(currentXPosition, currentYPosition);

            // Check if X has reached it's final position
            if (currentXPosition == finalXPosition)
            {
                HasReachedFinalPosition = true; // Both X and Y have reached the target
            }

            float direction = reverseRotation ? -1 : 1; // Determine rotation direction

            // Update the rotation based on the current state (reversing or not)
            if (isReversing)
            {
                // Reverse the rotation
                rotation -= direction * rotationSpeed / 1000;

                // Stop reversing once we reach or go below zero rotation
                if ((reverseRotation && rotation >= initialRotation) || (!reverseRotation && rotation <= initialRotation))
                {
                    rotation = initialRotation; // Reset to the initial rotation
                    isReversing = false; // Stop reversing
                    hasDoneRotation = false; // Reset the completion flag
                }
            }
            else
            {
                // Rotate forward until we hit the target rotation
                rotation += direction * rotationSpeed / 1000;


                // Once we reach the target rotation, prepare to reverse
                if ((reverseRotation && rotation <= initialRotation - targetRotation) || (!reverseRotation && rotation >= initialRotation + targetRotation))
                {
                    rotation = reverseRotation ? initialRotation - targetRotation : initialRotation + targetRotation;
                    isReversing = true; // Start reversing

                    if (!hasDoneRotation) // Increment only once
                    {
                        hasDoneRotation = true; // Set the completion flag
                    }
                }
            }

            // Update Rect to match the position change (if needed)
            Rect = new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Set the origin to the center of the sprite for proper rotation
            Vector2 origin = new Vector2(Width * 3, Height * 3);

            // Create a destination rectangle based on the current position and specified width and height
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);

            spriteBatch.Draw(texture, destinationRectangle, null, color, rotation, origin, SpriteEffects.None, 0);
        }

        public void ResetRotation()
        {
            hasDoneRotation = false; // Reset the completion flag
        }

        public void ResetPosition()
        {
            // Reset the position to the initial starting values
            currentXPosition = initialXPosition; // Set to the starting X position
            currentYPosition = initialYPosition; // Set to the starting Y position
            position = new Vector2(currentXPosition, currentYPosition); // Update the position vector
            HasReachedFinalPosition = false; // Reset the final position flag
        }
    }
}
