using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zune.Games.Sokoban.WindowsGame.Screens
{
    class BackgroundScreen : GameScreen
    {
        /// <summary>
        /// Private fields
        /// </summary>
        private Texture2D backgroundTexture;

        /// <summary>
        /// Constructor
        /// </summary>
        public BackgroundScreen()
        {
            // Set the transition times for this screen
            TransitionOnTime = TimeSpan.Zero;
            TransitionOffTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Loads graphics content for this screen.
        /// </summary>
        public override void LoadContent()
        {
            // Load the background texture from the main game 
            backgroundTexture = this.ScreenManager.Game.Content.Load<Texture2D>("background");
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            // Manually dispose the background texture
            backgroundTexture.Dispose();
        }

        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // Get the spritebatch from the screenmanager
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Begin drawing
            spriteBatch.Begin(SpriteBlendMode.None);

            // Draw the texture
            spriteBatch.Draw(backgroundTexture, new Vector2(), Color.White);
            
            // Stop drawing
            spriteBatch.End();

            // Draw fade background for transition
            if (this.ScreenState == ScreenState.TransitionOn)
                ScreenManager.FadeBackBufferToBlack(this.TransitionAlpha);
        }
    }
}
