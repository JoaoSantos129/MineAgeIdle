using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MineAgeIdle
{
    internal class StartView : View
    {
        ScaledSprite startBackgroundSprite;
        ScaledSprite logoSprite;
        ScaledSprite continueSprite;

        private GameManager gameManager;

        public StartView()
        {
            gameManager = GameManager.getInstance();

            Texture2D startBackgroundTexture = gameManager.Content.Load<Texture2D>("HUD\\Start\\StartBackground");
            startBackgroundSprite = new ScaledSprite(startBackgroundTexture, Vector2.Zero, Constants.DEFAULT_SCREEN_WIDTH, Constants.DEFAULT_SCREEN_HEIGHT);
            /*
            Texture2D logoTexture = gameManager.Content.Load<Texture2D>("HUD\\Start\\Logo");
            logoSprite = new ScaledSprite(logoTexture, new Vector2(610, 150), 700, 394);

            Texture2D continueTexture = gameManager.Content.Load<Texture2D>("HUD\\Start\\Continue");
            continueSprite = new ScaledSprite(continueTexture, new Vector2(622, 800), 676, 100);*/
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            spriteBatch.Draw(startBackgroundSprite.texture, startBackgroundSprite.Rect, Color.White);
            /*
            spriteBatch.Draw(logoSprite.texture, logoSprite.Rect, Color.White);
            // Only draw the "continue" sprite if it's currently visible
            if (tick)
            {
                spriteBatch.Draw(continueSprite.texture, continueSprite.Rect, Color.White);
            }*/
        }

        public override void Update(GameTime gameTime)
        {
            // Check if any key is pressed
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keys = currentKeyboardState.GetPressedKeys();

            if (keys.Length > 0)
            {
                gameManager.CurrentView = 10;
            }
        }
    }
}