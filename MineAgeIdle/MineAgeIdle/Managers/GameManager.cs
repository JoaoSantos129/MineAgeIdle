using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MineAgeIdle
{
    public class GameManager : Game
    {
        private static GameManager Instance;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private MovingSprite axeSprite;
        private MovingSprite pickaxeSprite;
        private MovingSprite tntSprite;
        private MovingSprite shovelSprite;

        private double elapsedTime = 0.0; // Variable to track elapsed time for continue button
        private bool tick = true; // Initial continue button visibility state

        private Random random = new Random();
        public int randomNumber;

        private int _currentView = 0;

        public double moneyToCollect = 125;
        public double coinsToCollectMaxAmount = 1000;
        public double woodInStock;
        public double woodAmountToStock = 4;
        public double woodPrice = 0.5;
        public double stoneInStock;
        public double stoneAmountToStock = 3;
        public double stonePrice = 10;
        public double gemsInStock;
        public double gemsAmountToStock = 2;
        public double gemsPrice = 100;
        public double treasuresInStock;
        public double treasuresAmountToStock = 1;
        public double treasuresPrice = 1000;

        public int CurrentView { get { return _currentView; } set { this._currentView = value; } }
        public double coinsAmount { get; set; } = 100;
        public double woodAmount { get; set; } = 100;
        public double axesAmount { get; set; } = 0;
        public double stoneAmount { get; set; } = 75;
        public double pickaxesAmount { get; set; } = 0;
        public double gemsAmount { get; set; } = 50;
        public double tntMachinesAmount { get; set; } = 0;
        public bool hasTriggeredGemCollection { get; set; } = false;
        public double treasuresAmount { get; set; } = 25;
        public double shovelsAmount { get; set; } = 0;

        // Views
        StartView startView;
        MenuView menuView;
        ShopView shopView;
        ForestView forestView;
        MountainView mountainView;
        MineView mineView;
        IslandView islandView;
        CasinoView casinoView;

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
            base.Initialize();

            // Generate a random number between 1 and 1000 during initialization
            randomNumber = random.Next(1, 1001);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize views
            startView = new StartView();
            menuView = new MenuView();
            shopView = new ShopView();
            forestView = new ForestView();
            mountainView = new MountainView();
            mineView = new MineView();
            islandView = new IslandView();
            casinoView = new CasinoView();

            // Initialize sprites for continuous rotation and harvesting
            Texture2D axeTexture = Content.Load<Texture2D>("HUD\\Forest\\Axe");
            axeSprite = new MovingSprite(axeTexture, new Vector2(1370, 620), 150, 150, Color.White, Color.Transparent, 0f, 15f, 90f, false, 0f, 1370f, 0f, false, 0f, 620f, false);

            Texture2D pickaxeTexture = Content.Load<Texture2D>("HUD\\Mountain\\Pickaxe");
            pickaxeSprite = new MovingSprite(pickaxeTexture, new Vector2(760, 760), 150, 150, Color.White, Color.Transparent, MathHelper.ToRadians(80), 10f, 90f, true, 0f, 760f, 0f, false, 0f, 760f, false);

            Texture2D tntTexture = Content.Load<Texture2D>("HUD\\Mine\\Tnt");
            tntSprite = new MovingSprite(tntTexture, new Vector2(1750, 600), 150, 150, Color.White, Color.Transparent, 0f, 22f, 50f, false, 6f, 1750f, 700f, true, 1f, 600f, false);

            Texture2D shovelTexture = Content.Load<Texture2D>("HUD\\Island\\Shovel");
            shovelSprite = new MovingSprite(shovelTexture, new Vector2(1800, 850), 268, 250, Color.White, Color.Transparent, 0f, 3f, 25f, true, 0f, 1800f, 0f, false, 0f, 850f, false);
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
                // Toggle the visibility state of the continue phrase
                tick = !tick;
                
                // Regenerate a random number between 1 and 1000
                randomNumber = random.Next(1, 1001);

                switch (randomNumber)
                {
                    case > 900:
                        woodInStock--;
                        moneyToCollect += woodPrice;
                        break;
                }

                // Reset the elapsed time
                elapsedTime = 0.0;
            }

            // Harvest resources based on rotation completion
            HarvestResources();

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
                case 40:
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
                    break;
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
                case 40:
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
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public float CalculatePrice(float price)
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

        private void HarvestResources()
        {
            // Check if axeSprite has completed a rotation for wood harvesting
            if (axesAmount > 0)
            {
                axeSprite.Update(); // Update rotation
                if (axeSprite.hasDoneRotation)
                {
                    woodAmount += axesAmount; // Harvest wood
                    axeSprite.ResetRotation();
                }
            }

            // Check if pickaxeSprite has completed a rotation for stone harvesting
            if (pickaxesAmount > 0)
            {
                pickaxeSprite.Update(); // Update rotation
                if (pickaxeSprite.hasDoneRotation)
                {
                    stoneAmount += pickaxesAmount; // Harvest stone
                    pickaxeSprite.ResetRotation();
                }
            }

            // Check if TNT has completed a rotation for gem harvesting
            if (tntMachinesAmount > 0)
            {
                tntSprite.Update(); // Update TNT sprite position

                if (tntSprite.HasReachedFinalPosition && !hasTriggeredGemCollection)
                {
                    // Collect gems immediately after TNT reaches its final position
                    gemsAmount += tntMachinesAmount;
                    hasTriggeredGemCollection = true;

                    // Reset TNT for next cycle
                    tntSprite.ResetPosition();
                    tntSprite.HasReachedFinalPosition = false; // Ensure the flag is reset for the next cycle
                }
                else if (!tntSprite.HasReachedFinalPosition)
                {
                    // If TNT hasn't reached its final position, reset the flag
                    hasTriggeredGemCollection = false;
                }
            }

            // Check if shovelSprite has completed a rotation for treasure harvesting
            if (shovelsAmount > 0)
            {
                shovelSprite.Update(); // Update rotation
                if (shovelSprite.hasDoneRotation)
                {
                    treasuresAmount += shovelsAmount; // Harvest treasure
                    shovelSprite.ResetRotation();
                }
            }
        }
    }
}