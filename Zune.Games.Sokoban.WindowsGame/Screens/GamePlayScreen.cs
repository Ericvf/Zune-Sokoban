using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zune.Games.Sokoban.WindowsGame.Transitions;

namespace Zune.Games.Sokoban.WindowsGame.Screens
{
    /// <summary>
    /// 
    /// </summary>
    class GamePlayScreen : GameScreen
    {
        private ContentManager content;
        private TileTransition tileTransition;
        private Texture2D blankTile;

        private LevelManager levelManager; 

        /// <summary>
        /// Constructor
        /// </summary>
        public GamePlayScreen()
        {
            // Set the transition times for this screen
            TransitionOnTime = TimeSpan.FromSeconds(0.1);
            TransitionOffTime = TimeSpan.FromSeconds(0.0);

            this.levelManager = new LevelManager();
            this.levelManager.FinishedLevel += new EventHandler<GameTimeEventArgs>(levelManager_FinishedLevel);

            this.tileTransition = new TileTransition(15, 16, 22, TimeSpan.FromSeconds(0.01), TimeSpan.FromSeconds(0.4));
            this.tileTransition.TransitionStop += new EventHandler<GameTimeEventArgs>(tileTransition_TransitionStop);
        }

        void levelManager_FinishedLevel(object sender, GameTimeEventArgs e)
        {
            this.tileTransition.Start(e.GameTime);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tileTransition_TransitionStop(object sender, GameTimeEventArgs e)
        {
            // Change the transition direction
            this.tileTransition.TransitionDirection *= -1;
         
            // Start only transition off
            if (this.tileTransition.TransitionDirection < 0)
            {
                this.tileTransition.Start(e.GameTime);
                this.levelManager.LoadNextLevel();
            }
        }

        /// <summary>
        /// Loads content for this screen
        /// </summary>
        public override void LoadContent()
        {
            // Store the content manager
            this.content = this.ScreenManager.Game.Content;

            // Loads a blank tile
            this.blankTile = this.content.Load<Texture2D>("blank");

            this.levelManager.LoadContent(this.content);
        }

        public override void HandleInput(List<Zune_Sokoban.GameKeys> input, GameTime gameTime)
        {
            this.levelManager.HandleInput(input, gameTime);
        }

        /// <summary>
        /// Updates the game logic
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="otherScreenHasFocus"></param>
        /// <param name="coveredByOtherScreen"></param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // Check if we need to start the transition
            if (this.ScreenState == ScreenState.TransitionOn)
            {
                // Start
                this.tileTransition.Start(gameTime);
                this.ScreenState = ScreenState.Active;
            }

            this.levelManager.Update(gameTime);
            this.tileTransition.Update(gameTime);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = this.ScreenManager.SpriteBatch;
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            this.levelManager.Draw(gameTime, spriteBatch);
            this.tileTransition.Draw(gameTime, spriteBatch, this.blankTile);
            

            spriteBatch.End();
        }

    }
}
