using System;
using System.Collections.Generic;
using System.Text;
using GameStateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Zune_Sokoban;

namespace Zune.Games.Sokoban.WindowsGame.Screens
{
    class SplashScreen : GameScreen
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreen()
        {
            // Set the transition times for this screen
            TransitionOnTime = TimeSpan.Zero;
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Handles the input, for this screen only the start button is relevant
        /// </summary>
        /// <param name="input"></param>
        public override void HandleInput(List<GameKeys> input)
        {
            // Check for start button
            if (input.Contains(GameKeys.Start))
            {
                this.ScreenManager.AddScreen(new MainMenuScreen());
                this.ExitScreen();
            }
        }

        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // Get the spritebatch from the screenmanager
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            // Begin drawing
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

            // Pulsate the size of the selected menu entry.
            double time = gameTime.TotalGameTime.TotalSeconds;
            float alpha = (0.5f + (float)Math.Sin(time * 6) * 0.25f) * 255;

            // Draw the texture
            spriteBatch.DrawString(ScreenManager.Font, "Press Start", new Vector2(), new Color(255, 255, 255, (byte)alpha));

            // Stop drawing
            spriteBatch.End();
        }
    }
}
