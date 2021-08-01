// .NET
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Game
using Zune.Games.Sokoban.WindowsGame.Screens;
using GameStateManagement;

namespace Zune.Games.Sokoban.WindowsGame
{
    /// <summary>
    /// This is the main type for the game. Most logic is abstracted to a series of components.
    /// </summary>
    public class Sokoban : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Private fields
        /// </summary>
        private GraphicsDeviceManager graphics;
        private ScreenManager screenManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public Sokoban()
        {
            // Create graphics device
            this.graphics = new GraphicsDeviceManager(this);

            // Set the dimensions of the window
            this.graphics.PreferredBackBufferWidth = 240;
            this.graphics.PreferredBackBufferHeight = 320;

            // Set the content directory
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Initialize screen manager
            this.InitializeScreenManager();

            // Initialize components
            base.Initialize();
        }

        /// <summary>
        /// Initializes the screen manager. It instanciates the screens for this game, adds them
        /// to the screenmanager, and most importantly this method adds the screenmanager to the 
        /// components collection of the Game instance.
        /// </summary>
        protected void InitializeScreenManager()
        {
            // Create the screen manager component
            this.screenManager = new ScreenManager(this);

            // Add the screens required for this game
            this.screenManager.AddScreen(new BackgroundScreen());
            this.screenManager.AddScreen(new MainMenuScreen());

            // Add the screen manager to the components collection
            this.Components.Add(this.screenManager);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen with the classic cornflower blue
            this.graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // This calls the draw method of each component
            base.Draw(gameTime);
        }
    }
}
