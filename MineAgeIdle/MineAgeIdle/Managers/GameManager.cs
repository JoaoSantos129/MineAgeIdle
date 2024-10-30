using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MineAgeIdle.Views;
using System;

namespace MineAgeIdle
{
    public class GameManager : Game
    {
        private static GameManager Instance;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private double elapsedTime = 0.0; // Variable to track elapsed time for continue button
        private bool tick = true; // Initial continue button visibility state

        private int _currentView = 0;

        public int CurrentView { get { return _currentView; } set { this._currentView = value; } }
        public double woodAmount { get; set; } = 0;
        public double axesAmount { get; set; } = 0;
        public double pickaxesAmount { get; set; } = 0;
        public double stoneAmount { get; set; } = 0;
        public double tntAmount { get; set; } = 0;
        public double diamondsAmount { get; set; } = 0;
        public double shovelsAmount { get; set; } = 0;
        public double treasuresAmount { get; set; } = 0;
        public double coinsAmount { get; set; } = 100f;

        // Views
        StartView startView;
        MenuView menuView;
        ShopView shopView;
        ForestView forestView;
        MountainView mountainView;
        /*MineView mineView;
        IslandView islandView;
        CasinoView casinoView;*/

        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = Constants.DEFAULT_SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = Constants.DEFAULT_SCREEN_HEIGHT;
            _graphics.ApplyChanges();
            Window.IsBorderless = true;
        }

        public static GameManager getInstance()
        {
            if (Instance == null)
            {
                Instance = new GameManager();
            }

            return Instance;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Views
            startView = new StartView();
            menuView = new MenuView();
            shopView = new ShopView();
            forestView = new ForestView();
            mountainView = new MountainView();
            /*mineView = new MineView();
            islandView = new IslandView();
            casinoView = new CasinoView();*/
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the elapsed time
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check if one second has passed
            if (elapsedTime >= Constants.TICK_DURATION)
            {
                // Toggle the visibility state
                tick = !tick;

                // Reset the elapsed time
                elapsedTime = 0.0;
            }

            switch (_currentView)
            {
                case 0:
                    startView.Update(gameTime);
                    break;
                case 10:
                    menuView.Update(gameTime);
                    shopView.Update(gameTime);
                    break;
                case 20:
                    menuView.Update(gameTime);
                    forestView.Update(gameTime);
                    break;
                case 30:
                    menuView.Update(gameTime);
                    mountainView.Update(gameTime);
                    break;
                /*case 40:
                    menuView.Update(gameTime);
                    mineView.Update(gameTime);
                    break;
                case 50:
                    menuView.Update(gameTime);
                    islandView.Update(gameTime);
                    break;
                case 60:
                    menuView.Update(gameTime);
                    casinoView.Update(gameTime);
                    break;*/
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            switch (_currentView)
            {
                case 0:
                    startView.Draw(gameTime, _spriteBatch, tick);
                    break;
                case 10:
                    shopView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;
                case 20:
                    forestView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;
                case 30:
                    mountainView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;
                /*case 40:
                    mineView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;
                case 50:
                    islandView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;
                case 60:
                    casinoView.Draw(gameTime, _spriteBatch, tick);
                    menuView.Draw(gameTime, _spriteBatch, tick);
                    break;*/
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public float CalculatePrice (float price)
        {
            return price * Constants.INFLATION_MULTIPLIYER;
        }

        public static string GetStringFormattedAmount(double originalAmount)
        {
            string formattedAmount;
            double temporaryAmout = 0.0;

            if (originalAmount < 1_000)
            {
                formattedAmount = originalAmount.ToString("0.##");
            }
            else if (originalAmount < 1_000_000)
            {
                temporaryAmout = Math.Truncate(originalAmount) / 1_000;
                formattedAmount = temporaryAmout.ToString() + " K";
            }
            else if (originalAmount < 1_000_000_000)
            {
                temporaryAmout = Math.Truncate(originalAmount / 1_000) / 1_000;
                formattedAmount = temporaryAmout.ToString() + " M";
            }
            else if (originalAmount < 1_000_000_000_000)
            {
                temporaryAmout = Math.Truncate(originalAmount / 1_000_000) / 1_000;
                formattedAmount = temporaryAmout.ToString() + " B";
            }
            else
            {
                temporaryAmout = Math.Truncate(originalAmount / 1_000_000_000) / 1_000;
                formattedAmount = temporaryAmout.ToString() + " T";
            }

            return formattedAmount;
        }
    }
}
