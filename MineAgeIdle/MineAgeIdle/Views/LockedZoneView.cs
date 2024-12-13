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
    internal class LockedZoneView : View
    {
        ScaledSprite lockedZoneBackgroundSprite;
        ScaledSprite backgroundFrameSprite;
        List<LockedZoneButton> lockedZoneButtons = new List<LockedZoneButton>();
        LockedZoneButton unlockShopButtonSprite;
        LockedZoneButton unlockForestButtonSprite;
        LockedZoneButton unlockMountainButtonSprite;
        LockedZoneButton unlockMineButtonSprite;
        LockedZoneButton unlockIslandButtonSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        public LockedZoneView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D lockedZoneBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\LockedZoneBackground");
            lockedZoneBackgroundSprite = new ScaledSprite(lockedZoneBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D backgroundFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            backgroundFrameSprite = new ScaledSprite(backgroundFrameTexture, new Vector2(Constants.MENU_WIDTH - 100, -100), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU + 200, Constants.DEFAULT_SCREEN_HEIGHT + 200);

            Texture2D unlockShopButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Shop\\UnlockShopButton");
            unlockShopButtonSprite = new LockedZoneButton(unlockShopButtonTexture, new Vector2(900, 650), 318, 55, Color.White, Color.Transparent, 1, true, 0);
            lockedZoneButtons.Add(unlockShopButtonSprite);

            Texture2D unlockForestButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\UnlockForestButton");
            unlockForestButtonSprite = new LockedZoneButton(unlockForestButtonTexture, new Vector2(900, 650), 318, 55, Color.White, Color.Transparent, 2, true, 0);
            lockedZoneButtons.Add(unlockForestButtonSprite);

            Texture2D unlockMountainButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Mountain\\UnlockMountainButton");
            unlockMountainButtonSprite = new LockedZoneButton(unlockMountainButtonTexture, new Vector2(900, 650), 318, 55, Color.White, Color.Transparent, 3, true, 0);
            lockedZoneButtons.Add(unlockMountainButtonSprite);

            Texture2D unlockMineButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\UnlockMineButton");
            unlockMineButtonSprite = new LockedZoneButton(unlockMineButtonTexture, new Vector2(900, 650), 318, 55, Color.White, Color.Transparent, 4, true, 0);
            lockedZoneButtons.Add(unlockMineButtonSprite);

            Texture2D unlockIslandButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Island\\UnlockIslandButton");
            unlockIslandButtonSprite = new LockedZoneButton(unlockIslandButtonTexture, new Vector2(900, 650), 318, 55, Color.White, Color.Transparent, 5, true, 0);
            lockedZoneButtons.Add(unlockIslandButtonSprite);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            spriteBatch.Draw(lockedZoneBackgroundSprite.texture, lockedZoneBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(backgroundFrameSprite.texture, backgroundFrameSprite.Rect, Color.White);
            spriteBatch.Draw(backgroundFrameSprite.texture, backgroundFrameSprite.Rect, Color.White);

            switch (gameManager._currentView)
            {
                case 10:
                    spriteBatch.DrawString(defaultFont, "After so many years of hard working just to have enough money for food and cigarrettes\n" +
                        "at the end of the month, you finally decided to stop smoking and save some money...\n\n" +
                        "You also find yourself lucky enough to find a great offer of a facility that you could use as your shop.\n\n" +
                        "Now it's your opportunity to invest in something that could actually change your\n" +
                        "life, it's a risky investement, but you feel like you have nothing else to lose...\n\n\n\n", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying the facility will cost you " + Constants.FACILITY_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
                case 20:
                    spriteBatch.DrawString(defaultFont, "Forest description", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying these shoes will cost you " + Constants.SHOES_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
                case 30:
                    spriteBatch.DrawString(defaultFont, "Mountain description", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying these shoes will cost you " + Constants.HIKING_EQUIPEMENT_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
                case 40:
                    spriteBatch.DrawString(defaultFont, "Mine description", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying these shoes will cost you " + Constants.DEMOLITION_EXPLOSIVE_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
                case 50:
                    spriteBatch.DrawString(defaultFont, "Island description", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying these shoes will cost you " + Constants.BOAT_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
                case 60:
                    spriteBatch.DrawString(defaultFont, "Casino description", new Vector2(Constants.MENU_WIDTH + 50, 400), Color.Black);
                    spriteBatch.DrawString(defaultFont, "Buying these shoes will cost you " + Constants.SUIT_COST + " Coins !", new Vector2(Constants.MENU_WIDTH + 50, 750), Color.Red);
                    break;
            }

            // Draw only the button matching the current view
            foreach (LockedZoneButton button in lockedZoneButtons)
            {
                if (button.Id * 10 == gameManager._currentView) // Check if the button matches the current view
                {
                    spriteBatch.Draw(button.texture, button.Rect, button.color);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (LockedZoneButton button in lockedZoneButtons)
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
                                if (gameManager.coinsAmount >= Constants.FACILITY_COST)
                                {
                                    gameManager.coinsAmount -= Constants.FACILITY_COST;
                                    gameManager.shopUnlocked = true;
                                }

                                break;

                            case 2:
                                if (gameManager.coinsAmount >= Constants.SHOES_COST)
                                {
                                    gameManager.coinsAmount -= Constants.SHOES_COST;
                                    gameManager.forestUnlocked = true;
                                }

                                break;

                            case 3:
                                if (gameManager.coinsAmount >= Constants.HIKING_EQUIPEMENT_COST)
                                {
                                    gameManager.coinsAmount -= Constants.HIKING_EQUIPEMENT_COST;
                                    gameManager.mountainUnlocked = true;
                                }

                                break;

                            case 4:
                                if (gameManager.coinsAmount >= Constants.DEMOLITION_EXPLOSIVE_COST)
                                {
                                    gameManager.coinsAmount -= Constants.DEMOLITION_EXPLOSIVE_COST;
                                    gameManager.mineUnlocked = true;
                                }

                                break;

                            case 5:
                                if (gameManager.coinsAmount >= Constants.BOAT_COST)
                                {
                                    gameManager.coinsAmount -= Constants.BOAT_COST;
                                    gameManager.casinoUnlocked = true;
                                }

                                break;

                            case 6:
                                if (gameManager.coinsAmount >= Constants.SUIT_COST)
                                {
                                    gameManager.coinsAmount -= Constants.SUIT_COST;
                                    gameManager.casinoUnlocked = true;
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
            foreach (var button in lockedZoneButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }
    }
}
