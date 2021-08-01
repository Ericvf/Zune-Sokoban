using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Zune_Sokoban;

namespace Zune.Games.Sokoban.WindowsGame
{
    class LevelManager
    {
        /// <summary>
        /// Events
        /// </summary>
        public event EventHandler<GameTimeEventArgs> FinishedLevel;

        /// <summary>
        /// Properties
        /// </summary>
        public bool Enabled { get; set; }
        
        /// <summary>
        /// Private fields
        /// </summary>
        private Texture2D levelTexture;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public LevelManager()
        {
        }

        /// <summary>
        /// Loads graphics for this manager
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            // Load the background texture from the main game 
            levelTexture = content.Load<Texture2D>("GameThumbnail");
        }

        /// <summary>
        /// Loads the next level
        /// </summary>
        public void LoadNextLevel()
        {
            this.Enabled = !this.Enabled;
        }

        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Don't do anything disabled
            if (!this.Enabled)
                return;
        }

        /// <summary>
        /// Draws the level data
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Don't do anything disabled
            if (!this.Enabled)
                return;

            // Draw the level
            spriteBatch.Draw(this.levelTexture, new Vector2(), Color.White);
        }

        public void HandleInput(List<Zune_Sokoban.GameKeys> input, GameTime gameTime)
        {
            if (input.Contains(GameKeys.Start))
            {
                if (this.FinishedLevel != null)
                    this.FinishedLevel(this, new GameTimeEventArgs() { GameTime = gameTime });
            }
        }
    }
}
