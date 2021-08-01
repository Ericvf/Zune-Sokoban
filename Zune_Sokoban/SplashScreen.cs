using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zune_Sokoban
{
    public class SplashScreen : GameScreen
    {
        private SpriteFont verdanaFont;
        private string text = "Press start";

        /// <summary>
        /// Initializes the screen
        /// </summary>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            // Attach input events
            this.input.OnKeyRelease += new InputManagerHandler(input_OnKeyRelease);

            this.spriteBatch = new SpriteBatch(graphics);

            // Call base
            base.Initialize(content, graphics);
        }

        /// <summary>
        /// Loads content from the pipeline
        /// </summary>
        public override void LoadContent()
        {
            // Load the sprint font
            verdanaFont = this.contentManager.Load<SpriteFont>("Verdana");

            // Base
            base.LoadContent();
        }

        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (verdanaFont == null)
                return;

            this.spriteBatch.Begin();
            this.spriteBatch.DrawString(this.verdanaFont, this.text, new Vector2(), Color.Black);
            this.spriteBatch.End();
        }

        /// <summary>
        /// Handles button releases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keys"></param>
        private void input_OnKeyRelease(InputManager sender, List<GameKeys> keys)
        {
            if (keys.Contains(GameKeys.Start))
                this.text = "pressed";
        }

    }
}