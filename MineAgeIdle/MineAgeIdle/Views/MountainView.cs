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
    internal class MountainView : View
    {
        ScaledSprite mountainBackgroundSprite;
        ScaledSprite buyPickaxeButtonFrameSprite;
        List<MountainButton> mountainButtons = new List<MountainButton>();
        MovingSprite pickaxeSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private float pickaxePrice = 10;

        public MountainView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D mountainBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Mountain\\MountainBackground");
            mountainBackgroundSprite = new ScaledSprite(mountainBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D buyPickaxeButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            buyPickaxeButtonFrameSprite = new ScaledSprite(buyPickaxeButtonFrameTexture, new Vector2(1240, 750), 353, 155);

            Texture2D buyPickaxeButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Mountain\\BuyPickaxeButton");
            MountainButton buyPickaxeButtonSprite = new MountainButton(buyPickaxeButtonTexture, new Vector2((buyPickaxeButtonFrameSprite.Width / 2) - (318 / 2) + buyPickaxeButtonFrameSprite.position.X, 10 + buyPickaxeButtonFrameSprite.position.Y), 318, 55, Color.White, Color.Transparent, 2);
            mountainButtons.Add(buyPickaxeButtonSprite);

            Texture2D pickaxeTexture = gameManager.Content.Load<Texture2D>("HUD\\Mountain\\Pickaxe");
            pickaxeSprite = new MovingSprite(pickaxeTexture, new Vector2(760, 760), 150, 150, Color.White, Color.Transparent, MathHelper.ToRadians(80), 10f, 90f, true);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringPickaxesAmount = GameManager.GetStringFormattedAmount(gameManager.pickaxesAmount);
            string stringNextPickaxePrice = GameManager.GetStringFormattedAmount(gameManager.CalculatePrice(pickaxePrice));

            spriteBatch.Draw(mountainBackgroundSprite.texture, mountainBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(buyPickaxeButtonFrameSprite.texture, buyPickaxeButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "Pickaxes : " + stringPickaxesAmount, new Vector2(buyPickaxeButtonFrameSprite.position.X + 25, buyPickaxeButtonFrameSprite.position.Y + 70), Color.Black);
            spriteBatch.DrawString(defaultFont, "Pickaxe price : " + stringNextPickaxePrice, new Vector2(buyPickaxeButtonFrameSprite.position.X + 25, buyPickaxeButtonFrameSprite.position.Y + 110), Color.Black);

            foreach (MountainButton button in mountainButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }

            if (gameManager.pickaxesAmount > 0)
            {
                pickaxeSprite.Update(); // Ensure Update is called
                pickaxeSprite.Draw(spriteBatch); // Draw using the new Draw method
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (MountainButton button in mountainButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Reset isOnView for all buttons before setting it for the pressed button
                    foreach (var btn in mountainButtons)
                    {
                        btn.IsOnView = false;
                    }

                    // Set isOnView for the currently pressed button
                    button.IsOnView = true;

                    switch (button.Id)
                    {
                        case 1:
                            gameManager.stoneAmount++;
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
            foreach (var button in mountainButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }

        private void ConfirmBuy()
        {
            if (gameManager.coinsAmount - gameManager.CalculatePrice(pickaxePrice) > 0)
            {
                gameManager.pickaxesAmount++;
                pickaxePrice = gameManager.CalculatePrice(pickaxePrice);
                gameManager.coinsAmount -= pickaxePrice;
            }
        }
    }
}