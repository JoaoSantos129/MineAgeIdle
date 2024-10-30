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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool tick)
        {
            spriteBatch.Draw(shopBackgroundSprite.texture, shopBackgroundSprite.Rect, Color.White);

            foreach (ShopButton button in shopButtons)
            {
                spriteBatch.Draw(button.texture, button.Rect, button.color);
            }
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
