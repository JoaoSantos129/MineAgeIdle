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
    internal class MineView : View
    {
        ScaledSprite mineBackgroundSprite;
        ScaledSprite buyTntMachineButtonFrameSprite;
        List<MineButton> mineButtons = new List<MineButton>();
        ScaledSprite tntMachineSprite;
        MovingSprite tntSprite;

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        private float tntMachinePrice = 150;
        private float tntReappearDelay = 1000f; // 1 second delay before TNT reappears
        private float tntDisappearTimer = 0f;
        private bool tntHidden = false;

        public MineView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            Texture2D mineBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\MineBackground");
            mineBackgroundSprite = new ScaledSprite(mineBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);

            Texture2D buyTntMachineButtonFrameTexture = gameManager.Content.Load<Texture2D>("HUD\\ButtonFrameTransparent");
            buyTntMachineButtonFrameSprite = new ScaledSprite(buyTntMachineButtonFrameTexture, new Vector2(1500, 50), 353, 155);

            Texture2D buyTntMachineButtonTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\BuyTntMachineButton");
            MineButton buyTntMachineButtonSprite = new MineButton(buyTntMachineButtonTexture, new Vector2((buyTntMachineButtonFrameSprite.Width / 2) - (318 / 2) + buyTntMachineButtonFrameSprite.position.X, 10 + buyTntMachineButtonFrameSprite.position.Y), 318, 55, Color.White, Color.Transparent, 1, true, 0);
            mineButtons.Add(buyTntMachineButtonSprite);

            Texture2D tntMachineTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\TntMachine");
            tntMachineSprite = new ScaledSprite(tntMachineTexture, new Vector2(1370, 500), 150, 150);

            Texture2D tntTexture = gameManager.Content.Load<Texture2D>("HUD\\Mine\\Tnt");
            tntSprite = new MovingSprite(tntTexture, new Vector2(1750, 600), 150, 150, Color.White, Color.Transparent, 0f, 22f, 50f, false, 6f, 1750f, 700f, true, 1f, 600f, false);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            string stringTntMachineAmount = GameManager.GetStringFormattedAmount(gameManager.tntMachinesAmount);
            string stringNextTntMachinePrice = GameManager.GetStringFormattedAmount(gameManager.CalculatePrice(tntMachinePrice));

            spriteBatch.Draw(mineBackgroundSprite.texture, mineBackgroundSprite.Rect, Color.White);
            spriteBatch.Draw(buyTntMachineButtonFrameSprite.texture, buyTntMachineButtonFrameSprite.Rect, Color.White);

            spriteBatch.DrawString(defaultFont, "TNT machines : " + stringTntMachineAmount, new Vector2(buyTntMachineButtonFrameSprite.position.X + 25, buyTntMachineButtonFrameSprite.position.Y + 70), Color.Black);
            spriteBatch.DrawString(defaultFont, "TNT machine price : " + stringNextTntMachinePrice, new Vector2(buyTntMachineButtonFrameSprite.position.X + 25, buyTntMachineButtonFrameSprite.position.Y + 110), Color.Black);

            foreach (MineButton button in mineButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }

            if (gameManager.tntMachinesAmount > 0)
            {
                tntMachineSprite.Update(); // Ensure Update is called
                tntMachineSprite.Draw(spriteBatch); // Draw using the new Draw method
                tntSprite.Update(); // Ensure Update is called
                // Draw TNT sprite only if it is not hidden
                if (!tntHidden)
                {
                    tntSprite.Draw(spriteBatch);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (MineButton button in mineButtons)
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
                                ConfirmBuy();
                                break;
                        }
                    });
                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    isLeftMousePressed = false;
                }
            }

            if (tntSprite.HasReachedFinalPosition && !gameManager.hasTriggeredGemCollection)
            {
                // Reset TNT for next cycle
                tntSprite.ResetPosition();
                tntSprite.HasReachedFinalPosition = false; // Ensure the flag is reset for the next cycle
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
                gameManager.tntMachinesAmount++;
                tntMachinePrice = gameManager.CalculatePrice(tntMachinePrice);
                gameManager.coinsAmount -= tntMachinePrice;
            }
        }
    }
}
