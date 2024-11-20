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
    internal class ShopView : View
    {
        ScaledSprite shopBackgroundSprite;
        ScaledSprite collectCoinsButtonFrameSprite;
        ScaledSprite restockWoodButtonFrameSprite;
        ScaledSprite restockStoneButtonFrameSprite;
        ScaledSprite restockGemsButtonFrameSprite;
        ScaledSprite restockTreasuresButtonFrameSprite;
        List<ShopButton> shopButtons = new List<ShopButton>();

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;



        public ShopView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D shopBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\ShopBackground");
            shopBackgroundSprite = new ScaledSprite(shopBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            // Coins button and frame
            Texture2D collectCoinsButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            collectCoinsButtonFrameSprite = new ScaledSprite(collectCoinsButtonFrameTexture, new Vector2(1000, 850), 375, 150);

            Texture2D collectCoinsButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\CollectCoinsButton");
            ShopButton collectCoinsButtonSprite = new ShopButton(collectCoinsButtonTexture, new Vector2((collectCoinsButtonFrameSprite.Width / 2) - (250 / 2) + collectCoinsButtonFrameSprite.position.X, 10 + collectCoinsButtonFrameSprite.position.Y), 250, 33, Color.White, Color.Transparent, 1, false, 1);
            shopButtons.Add(collectCoinsButtonSprite);

            // Wood button and frame
            Texture2D restockWoodButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            restockWoodButtonFrameSprite = new ScaledSprite(restockWoodButtonFrameTexture, new Vector2(450, 375), 375, 175);

            Texture2D restockWoodButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\RestockWoodButton");
            ShopButton restockWoodButtonSprite = new ShopButton(restockWoodButtonTexture, new Vector2((restockWoodButtonFrameSprite.Width / 2) - (250 / 2) + restockWoodButtonFrameSprite.position.X, 10 + restockWoodButtonFrameSprite.position.Y), 250, 33, Color.White, Color.Transparent, 2, false, 1);
            shopButtons.Add(restockWoodButtonSprite);

            // Stone button and frame
            Texture2D restockStoneButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            restockStoneButtonFrameSprite = new ScaledSprite(restockStoneButtonFrameTexture, new Vector2(1525, 375), 375, 175);

            Texture2D restockStoneButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\RestockStoneButton");
            ShopButton restockStoneButtonSprite = new ShopButton(restockStoneButtonTexture, new Vector2((restockStoneButtonFrameSprite.Width / 2) - (250 / 2) + restockStoneButtonFrameSprite.position.X, 10 + restockStoneButtonFrameSprite.position.Y), 250, 33, Color.White, Color.Transparent, 3, false, 1);
            shopButtons.Add(restockStoneButtonSprite);

            // Gems button and frame
            Texture2D restockGemsButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            restockGemsButtonFrameSprite = new ScaledSprite(restockGemsButtonFrameTexture, new Vector2(750, 150), 420, 175);

            Texture2D restockGemsButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\RestockGemsButton");
            ShopButton restockGemsButtonSprite = new ShopButton(restockGemsButtonTexture, new Vector2((restockGemsButtonFrameSprite.Width / 2) - (250 / 2) + restockGemsButtonFrameSprite.position.X, 10 + restockGemsButtonFrameSprite.position.Y), 250, 33, Color.White, Color.Transparent, 4, false, 1);
            shopButtons.Add(restockGemsButtonSprite);

            // Treasures button and frame
            Texture2D restockTreasuresButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            restockTreasuresButtonFrameSprite = new ScaledSprite(restockTreasuresButtonFrameTexture, new Vector2(1200, 150), 420, 175);

            Texture2D restockTreasuresButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\RestockTreasuresButton");
            ShopButton restockTreasuresButtonSprite = new ShopButton(restockTreasuresButtonTexture, new Vector2((restockTreasuresButtonFrameSprite.Width / 2) - (250 / 2) + restockTreasuresButtonFrameSprite.position.X, 10 + restockTreasuresButtonFrameSprite.position.Y), 250, 33, Color.White, Color.Transparent, 5, false, 1);
            shopButtons.Add(restockTreasuresButtonSprite);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringMoneyToCollect = GameManager.GetStringFormattedAmount(gameManager.moneyToCollect);
            string stringWoodInStock = GameManager.GetStringFormattedAmount(gameManager.woodInStock);
            string stringWoodAmountToStock = GameManager.GetStringFormattedAmount(gameManager.woodAmountToStock);
            string stringWoodPrice = GameManager.GetStringFormattedAmount(gameManager.woodPrice);
            string stringStoneInStock = GameManager.GetStringFormattedAmount(gameManager.stoneInStock);
            string stringStoneAmountToStock = GameManager.GetStringFormattedAmount(gameManager.stoneAmountToStock);
            string stringStonePrice = GameManager.GetStringFormattedAmount(gameManager.stonePrice);
            string stringGemsInStock = GameManager.GetStringFormattedAmount(gameManager.gemsInStock);
            string stringGemsAmountToStock = GameManager.GetStringFormattedAmount(gameManager.gemsAmountToStock);
            string stringGemsPrice = GameManager.GetStringFormattedAmount(gameManager.gemsPrice);
            string stringTreasuresInStock = GameManager.GetStringFormattedAmount(gameManager.treasuresInStock);
            string stringTreasuresAmountToStock = GameManager.GetStringFormattedAmount(gameManager.treasuresAmountToStock);
            string stringTreasuresPrice = GameManager.GetStringFormattedAmount(gameManager.treasuresPrice);

            spriteBatch.Draw(shopBackgroundSprite.texture, shopBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(collectCoinsButtonFrameSprite.texture, collectCoinsButtonFrameSprite.Rect, Color.White);
            spriteBatch.Draw(restockWoodButtonFrameSprite.texture, restockWoodButtonFrameSprite.Rect, Color.White);
            spriteBatch.Draw(restockStoneButtonFrameSprite.texture, restockStoneButtonFrameSprite.Rect, Color.White);
            spriteBatch.Draw(restockGemsButtonFrameSprite.texture, restockGemsButtonFrameSprite.Rect, Color.White);
            spriteBatch.Draw(restockTreasuresButtonFrameSprite.texture, restockTreasuresButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "Wood in stock : " + stringWoodInStock, new Vector2(restockWoodButtonFrameSprite.position.X + 25, restockWoodButtonFrameSprite.position.Y + 50), Color.Black);
            spriteBatch.DrawString(defaultFont, "Next stock : " + stringWoodAmountToStock, new Vector2(restockWoodButtonFrameSprite.position.X + 25, restockWoodButtonFrameSprite.position.Y + 90), Color.Black);
            spriteBatch.DrawString(defaultFont, "Wood price : " + stringWoodPrice, new Vector2(restockWoodButtonFrameSprite.position.X + 25, restockWoodButtonFrameSprite.position.Y + 130), Color.Black);

            spriteBatch.DrawString(defaultFont, "Stone in stock : " + stringStoneInStock, new Vector2(restockStoneButtonFrameSprite.position.X + 25, restockStoneButtonFrameSprite.position.Y + 50), Color.Black);
            spriteBatch.DrawString(defaultFont, "Next stock : " + stringStoneAmountToStock, new Vector2(restockStoneButtonFrameSprite.position.X + 25, restockStoneButtonFrameSprite.position.Y + 90), Color.Black);
            spriteBatch.DrawString(defaultFont, "Stone price : " + stringStonePrice, new Vector2(restockStoneButtonFrameSprite.position.X + 25, restockStoneButtonFrameSprite.position.Y + 130), Color.Black);

            spriteBatch.DrawString(defaultFont, "Gems in stock : " + stringGemsInStock, new Vector2(restockGemsButtonFrameSprite.position.X + 25, restockGemsButtonFrameSprite.position.Y + 50), Color.Black);
            spriteBatch.DrawString(defaultFont, "Next stock : " + stringGemsAmountToStock, new Vector2(restockGemsButtonFrameSprite.position.X + 25, restockGemsButtonFrameSprite.position.Y + 90), Color.Black);
            spriteBatch.DrawString(defaultFont, "Gems price : " + stringGemsPrice, new Vector2(restockGemsButtonFrameSprite.position.X + 25, restockGemsButtonFrameSprite.position.Y + 130), Color.Black);

            spriteBatch.DrawString(defaultFont, "Treasures in stock : " + stringTreasuresInStock, new Vector2(restockTreasuresButtonFrameSprite.position.X + 25, restockTreasuresButtonFrameSprite.position.Y + 50), Color.Black);
            spriteBatch.DrawString(defaultFont, "Next stock : " + stringTreasuresAmountToStock, new Vector2(restockTreasuresButtonFrameSprite.position.X + 25, restockTreasuresButtonFrameSprite.position.Y + 90), Color.Black);
            spriteBatch.DrawString(defaultFont, "Treasures price : " + stringTreasuresPrice, new Vector2(restockTreasuresButtonFrameSprite.position.X + 25, restockTreasuresButtonFrameSprite.position.Y + 130), Color.Black);

            spriteBatch.DrawString(defaultFont, "Coins to collect : " + stringMoneyToCollect, new Vector2(collectCoinsButtonFrameSprite.position.X + 25, collectCoinsButtonFrameSprite.position.Y + 50), Color.Black);
            spriteBatch.DrawString(defaultFont, "Random number : " + gameManager.randomNumber, new Vector2(collectCoinsButtonFrameSprite.position.X + 25, collectCoinsButtonFrameSprite.position.Y + 90), Color.Black);

            foreach (ShopButton button in shopButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (ShopButton button in shopButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is
                button.Update(gameTime); // Ensure cooldown logic is processed

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Use ProcessClick to handle button actions
                    button.ProcessClick(() =>
                    {
                        switch (button.Id)
                        {
                            case 1:
                                if (gameManager.moneyToCollect > 0)
                                {
                                    gameManager.coinsAmount += gameManager.moneyToCollect;
                                    gameManager.moneyToCollect = 0;
                                    button.ButtonOnCooldown(); // Activate cooldown for the restock button
                                }
                                break;

                            case 2:
                                if (gameManager.woodAmount > 0 && gameManager.woodAmount >= gameManager.woodAmountToStock)
                                {
                                    gameManager.woodInStock += gameManager.woodAmountToStock;
                                    gameManager.woodAmount -= gameManager.woodAmountToStock;
                                    button.ButtonOnCooldown(); // Activate cooldown for the restock button
                                }
                                break;

                            case 3:
                                if (gameManager.stoneAmount > 0 && gameManager.stoneAmount >= gameManager.stoneAmountToStock)
                                {
                                    gameManager.stoneInStock += gameManager.stoneAmountToStock;
                                    gameManager.stoneAmount -= gameManager.stoneAmountToStock;
                                    button.ButtonOnCooldown(); // Activate cooldown for the restock button
                                }
                                break;

                            case 4:
                                if (gameManager.gemsAmount > 0 && gameManager.gemsAmount >= gameManager.gemsAmountToStock)
                                {
                                    gameManager.gemsInStock += gameManager.gemsAmountToStock;
                                    gameManager.gemsAmount -= gameManager.gemsAmountToStock;
                                    button.ButtonOnCooldown(); // Activate cooldown for the restock button
                                }
                                break;

                            case 5:
                                if (gameManager.treasuresAmount > 0 && gameManager.treasuresAmount >= gameManager.treasuresAmountToStock)
                                {
                                    gameManager.treasuresInStock += gameManager.treasuresAmountToStock;
                                    gameManager.treasuresAmount -= gameManager.treasuresAmountToStock;
                                    button.ButtonOnCooldown(); // Activate cooldown for the restock button
                                }
                                break;
                        }
                    });
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
            foreach (var button in shopButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }
    }
}
