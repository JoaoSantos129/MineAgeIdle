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
    internal class MenuView : View
    {
        ScaledSprite menuBackgroundSprite;
        ScaledSprite currenciesBackgroundSprite;
        List<MenuButton> menuButtons = new List<MenuButton>();

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private int currentY = (Constants.DEFAULT_SCREEN_HEIGHT + 150 + 82 - (8 * (Constants.MENU_BUTTONS_HEIGHT + Constants.MENU_VERTICAL_SPACING))) / 2;

        public MenuView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D menuBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\MenuBackground");
            menuBackgroundSprite = new ScaledSprite(menuBackgroundTexture, Vector2.Zero, Constants.MENU_WIDTH, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D currenciesBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            currenciesBackgroundSprite = new ScaledSprite(currenciesBackgroundTexture, Vector2.Zero, Constants.MENU_WIDTH, 255);

            Texture2D menuShopTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\ShopButton");
            MenuButton menuShopSprite = new MenuButton(menuShopTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 1, true, 0);
            menuButtons.Add(menuShopSprite);
            menuShopSprite.IsOnView = true; // Shop is the starting view so it should be activated since the beginning

            IncrementHeightPosition();

            Texture2D menuForestTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\ForestButton");
            MenuButton menuForestSprite = new MenuButton(menuForestTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 2, true, 0);
            menuButtons.Add(menuForestSprite);

            IncrementHeightPosition();

            Texture2D menuMountainTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\MountainButton");
            MenuButton menuMountainSprite = new MenuButton(menuMountainTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 3, true, 0);
            menuButtons.Add(menuMountainSprite);

            IncrementHeightPosition();

            Texture2D menuMineTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\MineButton");
            MenuButton menuMineSprite = new MenuButton(menuMineTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 4, true, 0);
            menuButtons.Add(menuMineSprite);

            IncrementHeightPosition();

            Texture2D menuIslandTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\IslandButton");
            MenuButton menuIslandSprite = new MenuButton(menuIslandTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 5, true, 0);
            menuButtons.Add(menuIslandSprite);

            IncrementHeightPosition();

            Texture2D menuCasinoTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\CasinoButton");
            MenuButton menuCasinoSprite = new MenuButton(menuCasinoTexture, new Vector2((Constants.MENU_WIDTH - 176) / 2, currentY), 176, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 6, true, 0);
            menuButtons.Add(menuCasinoSprite);

            IncrementHeightPosition();

            Texture2D menuQuitTexture = gameManager.Content.Load<Texture2D>("HUD\\Menu\\Buttons\\QuitButton");
            MenuButton menuQuitSprite = new MenuButton(menuQuitTexture, new Vector2((Constants.MENU_WIDTH - 143) / 2, currentY), 143, Constants.MENU_BUTTONS_HEIGHT, Color.White, Color.Transparent, 7, true, 0);
            menuButtons.Add(menuQuitSprite);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringCoinsAmount = GameManager.GetStringFormattedAmount(Math.Round(gameManager.coinsAmount, 2));
            string stringWoodAmount = GameManager.GetStringFormattedAmount(gameManager.woodAmount);
            string stringStoneAmount = GameManager.GetStringFormattedAmount(gameManager.stoneAmount);
            string stringGemsAmount = GameManager.GetStringFormattedAmount(gameManager.gemsAmount);
            string stringTreasuresAmount = GameManager.GetStringFormattedAmount(gameManager.treasuresAmount);

            spriteBatch.Draw(menuBackgroundSprite.texture, menuBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(currenciesBackgroundSprite.texture, currenciesBackgroundSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "MA Coins : " + stringCoinsAmount, new Vector2(110, 15), Color.Black);
            spriteBatch.DrawString(defaultFont, "Wood : " + stringWoodAmount, new Vector2(110, 55), Color.Brown);
            spriteBatch.DrawString(defaultFont, "Stone : " + stringStoneAmount, new Vector2(110, 105), Color.Gray);
            spriteBatch.DrawString(defaultFont, "Gems : " + stringGemsAmount, new Vector2(110, 155), Color.Red);
            spriteBatch.DrawString(defaultFont, "Treasures : " + stringTreasuresAmount, new Vector2(110, 205), Color.Gold);

            foreach (MenuButton button in menuButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (MenuButton button in menuButtons)
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
                                gameManager.CurrentView = 10;
                                break;
                            case 2:
                                gameManager.CurrentView = 20;
                                break;
                            case 3:
                                gameManager.CurrentView = 30;
                                break;
                            case 4:
                                gameManager.CurrentView = 40;
                                break;
                            case 5:
                                gameManager.CurrentView = 50;
                                break;
                            case 6:
                                gameManager.CurrentView = 60;
                                break;
                            case 7:
                                gameManager.Exit();
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

        private void IncrementHeightPosition()
        {
            // Increment the Y position for the next button
            currentY += Constants.MENU_BUTTONS_HEIGHT + Constants.MENU_VERTICAL_SPACING;
        }
        
        // To put in GameManager
        private void UpdateButtonStates()
        {
            foreach (var button in menuButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id * 10);
            }
        }
    }
}
