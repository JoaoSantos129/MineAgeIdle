﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Drawing;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System;

namespace MineAgeIdle
{
    internal class MineView : View
    {
        ScaledSprite mineBackgroundSprite;
        ScaledSprite buyTntMachineButtonFrameSprite;
        List<MineButton> mineButtons = new List<MineButton>();
        ColoredSprite tntMachine;
        MovingSprite tntSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private float tntMachinePrice = 10;

        public MineView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            //Texture2D mineBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\MineBackground");
            Texture2D mineBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Start\\StartBackground");
            mineBackgroundSprite = new ScaledSprite(mineBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D buyTntMachineButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrame");
            buyTntMachineButtonFrameSprite = new ScaledSprite(buyTntMachineButtonFrameTexture, new Vector2(540, 750), 353, 155);

            //Texture2D buyTntMachineButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\BuyTntMachineButtonButton");
            Texture2D buyTntMachineButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\BuyAxeButton");
            MineButton buyTntMachineButtonSprite = new MineButton(buyTntMachineButtonTexture, new Vector2((buyTntMachineButtonFrameSprite.Width / 2) - (318 / 2) + buyTntMachineButtonFrameSprite.position.X, 10 + buyTntMachineButtonFrameSprite.position.Y), 318, 55, Color.White, Color.Transparent, 2);
            mineButtons.Add(buyTntMachineButtonSprite);

            //Texture2D pickaxeTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\TNT");
            Texture2D TntTexture = gameManager.Content.Load<Texture2D>("HUD\\Forest\\Axe");
            tntSprite = new MovingSprite(TntTexture, new Vector2(1370, 620), 150, 150, Color.White, Color.Transparent, 0f, 10f, 90f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringTntAmount = GameManager.GetStringFormattedAmount(gameManager.tntMachinesAmount);
            string stringNextTntPrice = GameManager.GetStringFormattedAmount(gameManager.CalculatePrice(tntMachinePrice));

            spriteBatch.Draw(mineBackgroundSprite.texture, mineBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(buyTntMachineButtonFrameSprite.texture, buyTntMachineButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "TNT machines : " + stringTntAmount, new Vector2(565, 820), Color.Black);
            spriteBatch.DrawString(defaultFont, "TNT machine price : " + stringNextTntPrice, new Vector2(565, 860), Color.Black);

            foreach (MineButton button in mineButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }

            if (gameManager.pickaxesAmount > 0)
            {
                tntSprite.Update(); // Ensure Update is called
                tntSprite.Draw(spriteBatch); // Draw using the new Draw method
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (MineButton button in mineButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Reset isOnView for all buttons before setting it for the pressed button
                    foreach (var btn in mineButtons)
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

            if (gameManager.axesAmount > 0)
            {
                // Update the pickaxe's position using the Update method of MovingSprite
                tntSprite.Update();

                // Increment gemsAmount if the pickaxe has completed a rotation
                if (tntSprite.hasDoneRotation)
                {
                    gameManager.gemsAmount += gameManager.tntMachinesAmount; // Increment gems amount
                    tntSprite.ResetRotation(); // Reset rotation status if needed
                }
            }
        }

        // To put in GameManager
        private void UpdateButtonStates()
        {
            foreach (var button in mineButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }

        private void ConfirmBuy()
        {
            if (gameManager.coinsAmount - gameManager.CalculatePrice(tntMachinePrice) > 0)
            {
                gameManager.axesAmount++;
                tntMachinePrice = gameManager.CalculatePrice(tntMachinePrice);
                gameManager.coinsAmount -= tntMachinePrice;
            }
        }
    }
}
