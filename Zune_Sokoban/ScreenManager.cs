using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zune_Sokoban
{
    public class ScreensManager : DrawableGameComponent
    {
        /// <summary>
        /// Private lists
        /// </summary>
        private List<GameScreen> screens;
        private List<GameScreen> activeScreens;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ScreensManager(Game game)
            : base(game)
        {
            // Initialize the screens
            this.InitializeScreens();
        }

        /// <summary>
        /// Initializes the screens in the manager
        /// </summary>
        private void InitializeScreens()
        {
            this.screens = new List<GameScreen>();
            this.activeScreens = new List<GameScreen>();
        }

        /// <summary>
        /// Adds a screen to the screenmanager
        /// </summary>
        /// <param name="gameScreen"></param>
        public void AddScreen(GameScreen gameScreen)
        {
            // Set the manager for this screen
            gameScreen.ScreenManager = this;

            // Add it to the collection
            this.screens.Add(gameScreen);
        }

        /// <summary>
        /// Removes a screen from the screenmanager
        /// </summary>
        /// <param name="gameScreen"></param>
        public void RemoveScreen(GameScreen gameScreen)
        {
            if (activeScreens.Contains(gameScreen))
                throw new ApplicationException("Unable to remove active screen");

            this.screens.Remove(gameScreen);
        }

        /// <summary>
        /// Activates a screen for drawing
        /// </summary>
        /// <param name="gameScreen"></param>
        public void ActivateScreen(GameScreen gameScreen)
        {
            if (!this.screens.Contains(gameScreen))
                throw new ApplicationException("GameScreen is not part of this ScreenManager");

            this.activeScreens.Add(gameScreen);
        }

        /// <summary>
        /// Initializes the game screens
        /// </summary>
        public override void Initialize()
        {
            foreach (GameScreen gameScreen in this.screens)
                gameScreen.Initialize(Game.Content, Game.GraphicsDevice);
        }

        /// <summary>
        /// Loads the content 
        /// </summary>
        protected override void LoadContent()
        {
            // Load all content for the game screens
            foreach (GameScreen gameScreen in this.screens)
                gameScreen.LoadContent();

            // Call base
            base.LoadContent();
        }

        /// <summary>
        /// Update the gamescreens
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Update all active screens
            foreach (GameScreen gameScreen in this.activeScreens)
                gameScreen.Update(gameTime);

            // 
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the gamescreens
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            // Update all active screens
            foreach (GameScreen gameScreen in this.activeScreens)
                gameScreen.Draw(gameTime);

            //
            base.Draw(gameTime);
        }
    }
}