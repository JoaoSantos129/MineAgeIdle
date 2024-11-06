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
    internal class CasinoView : View
    {
        ScaledSprite casinoBackgroundSprite;
        List<CasinoButton> casinoButtons = new List<CasinoButton>();

        private GameManager gameManager;
        public SpriteFont defaultFont;

        private bool isLeftMousePressed;

        public CasinoView()
        {
            gameManager = GameManager.getInstance();

            defaultFont = gameManager.Content.Load<SpriteFont>("Fonts\\DefaultFont");

            LoadContent();
        }

        private void LoadContent()
        {
            //Texture2D mountainBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Casino\\MountainBackground");
            Texture2D casinoBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Start\\StartBackground");
            casinoBackgroundSprite = new ScaledSprite(casinoBackgroundTexture, new Vector2(Constants.MENU_WIDTH, 0), Constants.BACKGROUND_WIDTH_VIEW_WITH_MENU, Constants.DEFAULT_SCREEN_HEIGHT);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            spriteBatch.Draw(casinoBackgroundSprite.texture, casinoBackgroundSprite.Rect, Color.White);

            foreach (CasinoButton button in casinoButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            UpdateButtonStates();

            foreach (CasinoButton button in casinoButtons)
            {
                button.IsHovered = button.Rect.Contains(mouseState.Position);   // Check where the mouse is

                if (button.IsHovered && mouseState.LeftButton == ButtonState.Pressed && !isLeftMousePressed)
                {
                    // When the button is pressed
                    isLeftMousePressed = true;

                    // Reset isOnView for all buttons before setting it for the pressed button
                    foreach (var btn in casinoButtons)
                    {
                        btn.IsOnView = false;
                    }

                    // Set isOnView for the currently pressed button
                    button.IsOnView = true;

                    /*switch (button.Id)
                    {
                        case 1:
                            gameManager.stoneAmount++;
                            break;
                        case 2:
                            ConfirmBuy();
                            break;
                    }*/
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
            foreach (var button in casinoButtons)
            {
                button.IsOnView = (gameManager.CurrentView == button.Id);
            }
        }
    }
}
