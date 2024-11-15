using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System;

namespace MineAgeIdle
{
    internal class ForestView : View
    {
        ScaledSprite forestBackgroundSprite;
        ScaledSprite buyAxeButtonFrameSprite;
        ScaledSprite breakButtonFrameSprite;
        List<ForestButton> forestButtons = new List<ForestButton>();
        MovingSprite axeSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private float axePrice = 10;

        public ForestView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D forestBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\ForestBackground");
            forestBackgroundSprite = new ScaledSprite(forestBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D buyAxeButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            buyAxeButtonFrameSprite = new ScaledSprite(buyAxeButtonFrameTexture, new Vector2(540, 750), 353, 155);

            Texture2D buyAxeButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\BuyAxeButton");
            ForestButton buyAxeButtonSprite = new ForestButton(buyAxeButtonTexture, new Vector2((buyAxeButtonFrameSprite.Width / 2) - (318 / 2) + buyAxeButtonFrameSprite.position.X, 10 + buyAxeButtonFrameSprite.position.Y), 318, 55, Color.White, Color.Transparent, 2);
            forestButtons.Add(buyAxeButtonSprite);

            Texture2D breakButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            breakButtonFrameSprite = new ScaledSprite(breakButtonFrameTexture, new Vector2(1420, 590), 200, 75);

            Texture2D breakButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\BreakButton");
            ForestButton breakButtonSprite = new ForestButton(breakButtonTexture, new Vector2((breakButtonFrameSprite.Width / 2) - (180 / 2) + breakButtonFrameSprite.position.X, (breakButtonFrameSprite.Height / 2) - (55 / 2) + breakButtonFrameSprite.position.Y), 180, 55, Color.White, Color.Transparent, 1);
            forestButtons.Add(breakButtonSprite);

            Texture2D axeTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\Axe");
            axeSprite = new MovingSprite(axeTexture, new Vector2(1370, 620), 150, 150, Color.White, Color.Transparent, 0f, 15f, 90f, false, 0f, 1370f, 0f, false, 0f, 620f, false);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringAxesAmount = GameManager.GetStringFormattedAmount(gameManager.axesAmount);
            string stringNextAxePrice = GameManager.GetStringFormattedAmount(gameManager.CalculatePrice(axePrice));

            spriteBatch.Draw(forestBackgroundSprite.texture, forestBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(breakButtonFrameSprite.texture, breakButtonFrameSprite.Rect, Color.White);
            spriteBatch.Draw(buyAxeButtonFrameSprite.texture, buyAxeButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "Axes : " + stringAxesAmount, new Vector2(buyAxeButtonFrameSprite.position.X + 25, buyAxeButtonFrameSprite.position.Y + 70), Color.Black);
            spriteBatch.DrawString(defaultFont, "Axe price : " + stringNextAxePrice, new Vector2(buyAxeButtonFrameSprite.position.X + 25, buyAxeButtonFrameSprite.position.Y + 110), Color.Black);

            foreach (ForestButton button in forestButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }

            if (gameManager.axesAmount > 0)
            {
                axeSprite.Update(); // Ensure Update is called
                axeSprite.Draw(spriteBatch); // Draw using the new Draw method
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (ForestButton button in forestButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Reset isOnView for all buttons before setting it for the pressed button
                    foreach (var btn in forestButtons)
                    {
                        btn.IsOnView = false;
                    }

                    // Set isOnView for the currently pressed button
                    button.IsOnView = true;

                    switch (button.Id)
                    {
                        case 1:
                            gameManager.woodAmount++;
                            break;
                        case 2:
                            ConfirmBuy();
                            break;
                    }
                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    isLeftMousePressed = false;
                }
            }
        }

        // To put in GameManager
        private void UpdateButtonStates()
        {
            foreach (var button in forestButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }

        private void ConfirmBuy()
        {
            if (gameManager.coinsAmount - gameManager.CalculatePrice(axePrice) >= 0)
            {
                gameManager.axesAmount++;
                axePrice = gameManager.CalculatePrice(axePrice);
                gameManager.coinsAmount -= axePrice;
            }
        }
    }
}