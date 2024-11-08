using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System;
using System.Reflection.Metadata;

namespace MineAgeIdle
{
    internal class IslandView : View
    {
        ScaledSprite islandBackgroundSprite;
        ScaledSprite buyShovelButtonFrameSprite;
        List<IslandButton> islandButtons = new List<IslandButton>();
        MovingSprite shovelSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private float shovelPrice = 10;

        public IslandView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D islandBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Island\\IslandBackground");
            islandBackgroundSprite = new ScaledSprite(islandBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D buyShovelButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            buyShovelButtonFrameSprite = new ScaledSprite(buyShovelButtonFrameTexture, new Vector2(540, 750), 353, 155);

            Texture2D buyShovelButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Island\\BuyShovelButton");
            IslandButton buyShovelButtonSprite = new IslandButton(buyShovelButtonTexture, new Vector2((buyShovelButtonFrameSprite.Width / 2) - (318 / 2) + buyShovelButtonFrameSprite.position.X, 10 + buyShovelButtonFrameSprite.position.Y), 318, 55, Color.White, Color.Transparent, 2);
            islandButtons.Add(buyShovelButtonSprite);

            Texture2D shovelTexture = gameManager.Content.Load<Texture2D>("HUD\\Island\\Shovel");
            shovelSprite = new MovingSprite(shovelTexture, new Vector2(1370, 620), 150, 150, Color.White, Color.Transparent, 0f, 5f, 90f, false);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringShovelsAmount = GameManager.GetStringFormattedAmount(gameManager.shovelsAmount);
            string stringNextShovelPrice = GameManager.GetStringFormattedAmount(gameManager.CalculatePrice(shovelPrice));

            spriteBatch.Draw(islandBackgroundSprite.texture, islandBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(buyShovelButtonFrameSprite.texture, buyShovelButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "Shovels : " + stringShovelsAmount, new Vector2(565, 820), Color.Black);
            spriteBatch.DrawString(defaultFont, "Shovel price : " + stringNextShovelPrice, new Vector2(565, 860), Color.Black);

            foreach (IslandButton button in islandButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }

            if (gameManager.shovelsAmount > 0)
            {
                shovelSprite.Update(); // Ensure Update is called
                shovelSprite.Draw(spriteBatch); // Draw using the new Draw method
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (IslandButton button in islandButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Reset isOnView for all buttons before setting it for the pressed button
                    foreach (var btn in islandButtons)
                    {
                        btn.IsOnView = false;
                    }

                    // Set isOnView for the currently pressed button
                    button.IsOnView = true;

                    switch (button.Id)
                    {
                        case 1:
                            gameManager.treasuresAmount++;
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
            foreach (var button in islandButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }

        private void ConfirmBuy()
        {
            if (gameManager.coinsAmount - gameManager.CalculatePrice(shovelPrice) > 0)
            {
                gameManager.shovelsAmount++;
                shovelPrice = gameManager.CalculatePrice(shovelPrice);
                gameManager.coinsAmount -= shovelPrice;
            }
        }
    }
}
